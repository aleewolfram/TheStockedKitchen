using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using TheStockedKitchen.Api.Config;
using TheStockedKitchen.Api.Utilities;
using TheStockedKitchen.Data.SpoonacularModel;
using TheStockedKitchen.Data.USDANutrientModel;
using TheStockedKitchen.Data.ViewModel;

namespace TheStockedKitchen.Api.Services
{
    public interface IIngredientService
    {
        Task<List<FoodDataVM>> GetFullFoodDataResultsAsync(string search);
    }
    
    public class IngredientService : IIngredientService
    {

        private readonly TheStockedKitchenConfiguration _uSDANutrientDBConfiguration;

        private readonly IUnitService _unitService;

        public IngredientService(TheStockedKitchenConfiguration uSDANutrientDBConfiguration, IUnitService unitService)
        {
            _uSDANutrientDBConfiguration = uSDANutrientDBConfiguration;
            _unitService = unitService;
        }

        public async Task<List<FoodDataVM>> GetFullFoodDataResultsAsync(string search)
        {
            // Keeping this here so I don't spam the limited Spoonacular API
            if (true)
            {
                using (StreamReader r = new StreamReader("SampleData/BananaSearchResults.json"))
                {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<FoodDataVM>>(json);
                }
            }
            else
            {
                try
                {
                    List<FoodDataVM> FoodNames = await GetFoodAsync(search);
                    List<FoodDataVM> FoodInfo = await GetFoodInfoAsync(search);

                    var foodDataVMs = from names in FoodNames.Where(n => n.NBDNumber != 0)
                                      join info in FoodInfo.Where(n => n.NBDNumber != 0)
                                      on names.NBDNumber equals info.NBDNumber into gj
                                      from subInfo in gj.DefaultIfEmpty()
                                      where names.Name.ToLower() == subInfo?.Name.ToLower() || names.NBDNumber == subInfo?.NBDNumber || subInfo == null
                                      select new FoodDataVM
                                      {
                                          FDCId = subInfo?.FDCId ?? 0,
                                          NBDNumber = subInfo?.NBDNumber ?? names.NBDNumber,
                                          Name = names.Name,
                                          FoodCategory = subInfo?.FoodCategory ?? string.Empty,
                                          Image = names.Image,
                                          FoodNutrients = subInfo?.FoodNutrients ?? new List<FoodNutrientVM>()
                                      };

                    return foodDataVMs.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred with getting food data.", ex);
                }
            }
        }

        // Get cleansed food names from Spoonacular
        private async Task<List<FoodDataVM>> GetFoodAsync(string search)
        {
            string apiUrl = "https://api.spoonacular.com/food/ingredients/search";
            string apiKey = _uSDANutrientDBConfiguration.ApiKey.SpoonacularApiKey;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var payload = new
                {
                    query = search,
                    number = 20,
                    apiKey
                };
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);

                var requestUri = $"{apiUrl}?{QueryStringBuilder.BuildQueryString(payload)}";

                var response = await httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    IngredientSearchResults ingredientSearchResults = JsonConvert.DeserializeObject<IngredientSearchResults>(content);
                    List<FoodDataVM> foodDataVMs = ingredientSearchResults.results.Select(r => new FoodDataVM(r)).ToList();
                    return foodDataVMs;

                }
                return null;
            }
        }

        // Query USDA for extra food information since thier API is less limiting for free versions
        private async Task<List<FoodDataVM>> GetFoodInfoAsync(string search)
        {
            string apiUrl = "https://api.nal.usda.gov/fdc/v1/foods/search";
            string apiKey = _uSDANutrientDBConfiguration.ApiKey.USDAApiKey;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var payload = new 
                { 
                    query = search,
                    dataType = new[] { "SR Legacy" }
                };
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);

                var requestUri = $"{apiUrl}?api_key={apiKey}";

                var response = await httpClient.PostAsync(requestUri, new StringContent(jsonPayload, Encoding.UTF32, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JsonNode data = System.Text.Json.JsonSerializer.Deserialize<JsonNode>(content);
                    if (data != null)
                    {
                        if (data["foods"] != null)
                        {
                            List<FoodData> foodDatas = JsonConvert.DeserializeObject<List<FoodData>>(data["foods"].ToJsonString());
                            List<FoodDataVM> foodDataVMs = foodDatas.Select(f => new FoodDataVM(f)).ToList();
                            foodDataVMs = await _unitService.ApplyUnitInformationAsync(foodDataVMs);
                            return foodDataVMs;
                        }
                    }
                }
                return null;
            }
        }
    }
}
