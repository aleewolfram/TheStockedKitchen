using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using TheStockedKitchen.Api.Config;

namespace TheStockedKitchen.Api.Services
{
    public interface IUSDANutrientDBService
    {
        Task<object> GetFoodDataAsync(string search);
    }
    public class USDANutrientDBService : IUSDANutrientDBService
    {

        private readonly TheStockedKitchenConfiguration _uSDANutrientDBConfiguration;

        public USDANutrientDBService(TheStockedKitchenConfiguration uSDANutrientDBConfiguration)
        {
            _uSDANutrientDBConfiguration = uSDANutrientDBConfiguration;
        }

        public async Task<object> GetFoodDataAsync(string search)
        {
            string apiUrl = "https://api.nal.usda.gov/fdc/v1/foods/search";
            string apiKey = _uSDANutrientDBConfiguration.ApiKey.USDAApiKey;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var payload = new 
                { 
                    query = search,
                    dataType = new[] { "Foundation" },
                };
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);

                var requestUri = $"{apiUrl}?api_key={apiKey}";

                var response = await httpClient.PostAsync(requestUri, new StringContent(jsonPayload, Encoding.UTF32, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JsonNode data = System.Text.Json.JsonSerializer.Deserialize<JsonNode>(content);
                    if(data != null)
                    {
                        var food = data["foods"];
                        return food;

                    }
                }
                return null;
            }
        }
    }
}
