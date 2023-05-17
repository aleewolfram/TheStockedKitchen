using TheStockedKitchen.Data.ViewModel;
using TheStockedKitchen.Api.Config;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using TheStockedKitchen.Data.SpoonacularModel;
using TheStockedKitchen.Api.Utilities;

namespace TheStockedKitchen.Api.Services
{
    public interface IRecipeService
    {
        Task<List<RecipeVM>> GetRecipesAsync(string ingredients);
    }
    public class RecipeService : IRecipeService
    {
        private readonly TheStockedKitchenConfiguration _uSDANutrientDBConfiguration;

        public RecipeService(TheStockedKitchenConfiguration uSDANutrientDBConfiguration)
        {
            _uSDANutrientDBConfiguration = uSDANutrientDBConfiguration;
        }

        public async Task<List<RecipeVM>> GetRecipesAsync(string ingredients)
        {
            //Keeping this here so I don't spam the limited Spoonacular API
            if (true)
            {
                using (StreamReader r = new StreamReader("SampleData/BananaRecipeSearchResults.json"))
                {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<RecipeVM>>(json);
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
    }
}
