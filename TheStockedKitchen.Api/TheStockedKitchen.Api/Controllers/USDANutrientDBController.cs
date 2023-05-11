using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TheStockedKitchen.Api.Services;
using TheStockedKitchen.Data.ViewModel;

namespace SCGPlanningTool.Api.Controllers
{
    [ApiController]
    public class USDANutrientDBController : ControllerBase
    {
        private readonly IUSDANutrientDBService _USDANutrientDBService;
        public USDANutrientDBController(IUSDANutrientDBService USDANutrientDBService)
        {
            _USDANutrientDBService = USDANutrientDBService;
        }

        [HttpPost("GetFoodData")]
        [OpenApiOperation("GetFoodData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<FoodDataVM>> GetFoodDataAsync(string search)
        {
            return await _USDANutrientDBService.GetFoodDataAsync(search);
        }
    }
}
