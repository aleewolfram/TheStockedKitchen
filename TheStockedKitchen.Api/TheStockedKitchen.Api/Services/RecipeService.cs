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
        Task<RecipeDetailVM> GetRecipeDetailAsync(RecipeVM recipeVM, string user);
        Task<int> MarkRecipeAsMadeAsync(RecipeVM recipeVM, string user);
        Task<bool> UndoMarkRecipeAsMadeAsync(int recipeMadeId, string user);
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
                        ignorePantry = true,
                        apiKey

                    };
                    var jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);

                    var requestUri = $"{apiUrl}?{QueryStringBuilder.BuildQueryString(payload)}";

                    var response = await httpClient.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        List<Recipe> recipes = System.Text.Json.JsonSerializer.Deserialize<List<Recipe>>(content);
                        return recipes.Select(r => new RecipeVM(r)).ToList();
                    }

                    return null;
                }
            }
        }
        
        public async Task<RecipeDetailVM> GetRecipeDetailAsync(RecipeVM recipeVM, string user)
        {
            // Keeping this here so I don't spam the limited Spoonacular API
            if (true)
            {
                using (StreamReader r = new StreamReader("SampleData/RecipeDetailResult.json"))
                {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<RecipeDetailVM>(json);
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

                        List<IngredientCompareVM> ingredientCompareVMs = (  from d in recipeDetail.extendedIngredients
                                                                            from f in foodStocks
                                                                            where (d.nameClean == f.Name) || (d.name == f.Name)
                                                                            select new IngredientCompareVM
                                                                            {
                                                                                RecipeIngredientName = !string.IsNullOrEmpty(d.nameClean) ? d.nameClean : d.name,
                                                                                RecipeIngredientQuantity = d.amount,
                                                                                RecipeIngredientUnit = d.unit.ToUpper(),
                                                                                RecipeIngredientUnitAbbreviation = d.unit.ToUpper(),
                                                                                PantryIngredientId = f.FoodStockId,
                                                                                PantryIngredientName = f.Name,
                                                                                PantryIngredientQuantity = f.Quantity,
                                                                                PantryIngredientUnit = f.Unit,
                                                                                PantryIngredientUnitAbbreviation = f.UnitAbbreviation
                                                                            }
                                                                        ).ToList();


                        RecipeDetailVM recipeDetailVM = new RecipeDetailVM()
                        {
                            SourceUrl = recipeDetail.sourceUrl,
                            Title = recipeDetail.title,
                            Image = recipeDetail.image,
                            Summary = recipeDetail.summary,
                            IngredientCompareVMs = new List<IngredientCompareVM>()
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

        public async Task<int> MarkRecipeAsMadeAsync(RecipeVM recipeVM, string user)
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

                return recipeMade.RecipeMadeId;
            }
            catch
            {
                return -1;
            }
            
        }

        public async Task<bool> UndoMarkRecipeAsMadeAsync(int recipeMadeId, string user)
        {
            try
            {
                RecipeMade recipeMade = await _dbContext.RecipesMade.Where(m => m.RecipeMadeId == recipeMadeId && m.User == user).SingleAsync();

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
