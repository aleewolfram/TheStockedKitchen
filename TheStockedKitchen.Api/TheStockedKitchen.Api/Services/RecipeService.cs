using Microsoft.EntityFrameworkCore;
using TheStockedKitchen.Db;
using TheStockedKitchen.Data.Model;
using TheStockedKitchen.Data.ViewModel;
using TheStockedKitchen.Api.Config;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using TheStockedKitchen.Data.SpoonacularModel;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TheStockedKitchen.Api.Services
{
    public interface IRecipeService
    {
        Task<List<Recipe>> GetRecipesAsync(string ingredients);
    }
    public class RecipeService : IRecipeService
    {
        private readonly TheStockedKitchenConfiguration _uSDANutrientDBConfiguration;

        public RecipeService(TheStockedKitchenConfiguration uSDANutrientDBConfiguration)
        {
            _uSDANutrientDBConfiguration = uSDANutrientDBConfiguration;
        }

        public async Task<List<Recipe>> GetRecipesAsync(string ingredients)
        {
            string apiUrl = "https://api.spoonacular.com/recipes/findByIngredients";
            string apiKey = _uSDANutrientDBConfiguration.ApiKey.SpoonacularApiKey;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var payload = new
                {
                    ingredients = ingredients.Split(',').Select(i => i.Trim()).ToList(),
                    number = 10,
                    ignorePantry = true,
                    apiKey

                };
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);

                var requestUri = $"{apiUrl}?{BuildQueryString(payload)}";

                var response = await httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return System.Text.Json.JsonSerializer.Deserialize<List<Recipe>>(content);
                }

                return null;
            }
        }

        private string BuildQueryString(object parameters)
        {
            var keyValuePairs = new List<string>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(parameters))
            {
                var value = property.GetValue(parameters);
                if (value is IList<string> listValue)
                {
                    keyValuePairs.AddRange(listValue.Select(item => $"{property.Name}={Uri.EscapeDataString(item)}"));
                }
                else
                {
                    keyValuePairs.Add($"{property.Name}={Uri.EscapeDataString(value.ToString())}");
                }
            }

            return string.Join("&", keyValuePairs);
        }
    }
}
