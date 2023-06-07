using TheStockedKitchen.Data.ViewModel;
using TheStockedKitchen.Api.Config;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using TheStockedKitchen.Data.SpoonacularModel;
using TheStockedKitchen.Api.Utilities;
using TheStockedKitchen.Data.Model;
using TheStockedKitchen.Db;
using Microsoft.EntityFrameworkCore;

namespace TheStockedKitchen.Api.Services
{
    public interface IRecipeService
    {
        Task<List<RecipeVM>> GetRecipesAsync(string ingredients);
        Task<List<RecipeVM>> GetRecipesMadeAsync(string user);
        Task<RecipeDetailVM> GetRecipeDetailAsync(RecipeVM recipeVM, string user);
        Task<bool> MarkRecipeAsMadeAsync(RecipeVM recipeVM, string user);
        Task<bool> UndoMarkRecipeAsMadeAsync(int recipeId, string user);
    }
    public class RecipeService : IRecipeService
    {
        private readonly TheStockedKitchenConfiguration _uSDANutrientDBConfiguration;

        private readonly IFoodStockService _foodStockService;

        private readonly IUnitService _unitService;

        private readonly AppDBContext _dbContext;

        public RecipeService(TheStockedKitchenConfiguration uSDANutrientDBConfiguration, IFoodStockService foodStockService, IUnitService unitService, AppDBContext dbContext)
        {
            _uSDANutrientDBConfiguration = uSDANutrientDBConfiguration;
            _foodStockService = foodStockService;
            _unitService = unitService;
            _dbContext = dbContext;
        }

        public async Task<List<RecipeVM>> GetRecipesAsync(string ingredients)
        {
            //Keeping this here so I don't spam the limited Spoonacular API
            if (true)
            {
                if(ingredients == "banana")
                {
                    using (StreamReader r = new StreamReader("SampleData/BananaRecipeSearchResults.json"))
                    {
                        string json = r.ReadToEnd();
                        return JsonConvert.DeserializeObject<List<RecipeVM>>(json);
                    }
                }
                else if(ingredients == "banana, peanut butter, chocolate chips" || 
                        ingredients == "banana, chocolate chips, peanut butter" || 
                        ingredients == "chocolate chips, banana, peanut butter" ||
                        ingredients == "chocolate chips, peanut butter, banana" ||
                        ingredients == "peanut butter, banana, chocolate chips" ||
                        ingredients == "peanut butter, chocolate chips, banana")
                {
                    using (StreamReader r = new StreamReader("SampleData/BananaChocolatePeanutButterRecipeSearchResults.json"))
                    {
                        string json = r.ReadToEnd();
                        return JsonConvert.DeserializeObject<List<RecipeVM>>(json);
                    }
                }
                else
                {
                    return new List<RecipeVM>();
                }
            }
            else
            {
                string apiUrl = "https://api.spoonacular.com/recipes/findByIngredients";
                string apiKey = _uSDANutrientDBConfiguration.ApiKey.SpoonacularApiKey;

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var payload = new
                    {
                        ingredients = ingredients.Split(',').Select(i => i.Trim()).ToList(),
                        number = 20,
                        ignorePantry = false,
                        ranking = 2,
                        apiKey

                    };
                    var jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);

                    var requestUri = $"{apiUrl}?{QueryStringBuilder.BuildQueryString(payload)}";

                    var response = await httpClient.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        List<Recipe> recipes = System.Text.Json.JsonSerializer.Deserialize<List<Recipe>>(content);
                        List<RecipeVM> recipeVMs = recipes.Select(r => new RecipeVM(r)).ToList();
                        foreach(RecipeVM recipeVM in recipeVMs)
                        {
                            recipeVM.TimesMade = await _dbContext.RecipesMade.Where(r => r.SpoonacularRecipeId == recipeVM.RecipeId).CountAsync();
                        }

                        return recipeVMs;
                    }

                    return null;
                }
            }
        }

        public async Task<List<RecipeVM>> GetRecipesMadeAsync(string user)
        {
            return await (from r in _dbContext.RecipesMade
                      group r by r.SpoonacularRecipeId
                      into grp
                      select new RecipeVM
                      {
                          RecipeId = grp.Key,
                          Title = grp.First().Title,
                          Image = grp.First().Image,
                          TimesMade = grp.Count()
                      }).ToListAsync();
        }

        public async Task<RecipeDetailVM> GetRecipeDetailAsync(RecipeVM recipeVM, string user)
        {
            // Keeping this here so I don't spam the limited Spoonacular API
            if (true)
            {
                if(recipeVM.RecipeId == 647093)
                {
                    using (StreamReader r = new StreamReader("SampleData/PeanutButterCupsRecipeDetailResult.json"))
                    {
                        string json = r.ReadToEnd();
                        return JsonConvert.DeserializeObject<RecipeDetailVM>(json);
                    }
                }
                else
                {
                    using (StreamReader r = new StreamReader("SampleData/BananaDipsRecipeDetailResult.json"))
                    {
                        string json = r.ReadToEnd();
                        return JsonConvert.DeserializeObject<RecipeDetailVM>(json);
                    }
                }
            }
            else
            {
                string apiUrl = $"https://api.spoonacular.com/recipes/{recipeVM.RecipeId}/information";
                string apiKey = _uSDANutrientDBConfiguration.ApiKey.SpoonacularApiKey;

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var payload = new
                    {
                        apiKey

                    };
                    var jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);

                    var requestUri = $"{apiUrl}?{QueryStringBuilder.BuildQueryString(payload)}";

                    var response = await httpClient.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        
                        RecipeDetail recipeDetail = System.Text.Json.JsonSerializer.Deserialize<RecipeDetail>(content);
                        
                        List<FoodStock> foodStocks = await _foodStockService.GetFoodStockAsync(user);

                        var joinByNameClean = from d in recipeDetail.extendedIngredients
                                              join f in foodStocks on d.nameClean equals f.Name into gj
                                              from f in gj.DefaultIfEmpty()
                                              select new { d, f };

                        var joinByName = from d in recipeDetail.extendedIngredients
                                         join f in foodStocks on d.name equals f.Name into gj
                                         from f in gj.DefaultIfEmpty()
                                         select new { d, f };

                        var joinByImg = from d in recipeDetail.extendedIngredients
                                        join f in foodStocks on d.image equals f.Image.Split('/').Last() into gj
                                        from f in gj.DefaultIfEmpty()
                                        select new { d, f };

                        List<IngredientCompareVM> ingredientCompareVMs = (
                                                                            from j1 in joinByNameClean
                                                                            join j2 in joinByName on j1.d equals j2.d into gj1
                                                                            from j2 in gj1.DefaultIfEmpty()
                                                                            join j3 in joinByImg on j1.d equals j3.d into gj2
                                                                            from j3 in gj2.DefaultIfEmpty()
                                                                            select new IngredientCompareVM
                                                                            {
                                                                                RecipeIngredientName = !string.IsNullOrEmpty(j1.d.nameClean) ? j1.d.nameClean : j1.d.name,
                                                                                RecipeIngredientQuantity = j1.d.amount,
                                                                                RecipeIngredientUnit = j1.d.unit.ToUpper(),
                                                                                RecipeIngredientUnitAbbreviation = j1.d.unit.ToUpper(),
                                                                                PantryIngredientId = j1.f == null ? (j2?.f?.FoodStockId ?? (j3?.f?.FoodStockId ?? 0)) : j1.f.FoodStockId,
                                                                                PantryIngredientName = j1.f?.Name ?? (j2?.f?.Name ?? j3?.f?.Name),
                                                                                PantryIngredientQuantity = j1.f == null ? (j2?.f?.Quantity ?? (j3?.f?.Quantity ?? 0)) : j1.f.Quantity,
                                                                                PantryIngredientUnit = j1.f?.Unit ?? (j2?.f?.Unit ?? j3?.f?.Unit),
                                                                                PantryIngredientUnitAbbreviation = j1.f?.UnitAbbreviation ?? (j2?.f?.UnitAbbreviation ?? j3?.f?.UnitAbbreviation)
                                                                            }
                                                                        ).ToList();


                        RecipeDetailVM recipeDetailVM = new RecipeDetailVM()
                        {
                            SourceUrl = recipeDetail.sourceUrl,
                            Title = recipeDetail.title,
                            Image = recipeDetail.image,
                            Summary = recipeDetail.summary,
                            TimesMade = await _dbContext.RecipesMade.Where(r => r.SpoonacularRecipeId == recipeVM.RecipeId).CountAsync(),
                            IngredientCompareVMs = new List<IngredientCompareVM>(),
                            Instructions = recipeDetail.analyzedInstructions.SelectMany(i => i.steps.Select(s => s.step)).ToList()
                        };

                        foreach(IngredientCompareVM ingredientCompareVM in ingredientCompareVMs)
                        {
                            IngredientCompareVM convertedIngredientCompareVM = await _unitService.GetPantryIngredientRemaining(ingredientCompareVM);
                            recipeDetailVM.IngredientCompareVMs.Add(convertedIngredientCompareVM);
                        }
                        
                        return recipeDetailVM;
                    }

                    return null;
                }
            }
        }

        public async Task<bool> MarkRecipeAsMadeAsync(RecipeVM recipeVM, string user)
        {
            try
            {
                RecipeMade recipeMade = new RecipeMade()
                {
                    SpoonacularRecipeId = recipeVM.RecipeId,
                    Title = recipeVM.Title,
                    Image = recipeVM.Image,
                    CreatedDate = DateTime.Now,
                    User = user
                };

                await _dbContext.RecipesMade.AddAsync(recipeMade);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<bool> UndoMarkRecipeAsMadeAsync(int recipeId, string user)
        {
            try
            {
                RecipeMade recipeMade = await _dbContext.RecipesMade.Where(m => m.SpoonacularRecipeId == recipeId && m.User == user).OrderByDescending(m => m.CreatedDate).FirstAsync();

                _dbContext.RecipesMade.Remove(recipeMade);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
