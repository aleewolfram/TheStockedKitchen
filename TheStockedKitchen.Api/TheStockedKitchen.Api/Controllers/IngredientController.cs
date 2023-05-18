using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TheStockedKitchen.Api.Services;
using TheStockedKitchen.Data.ViewModel;

namespace SCGPlanningTool.Api.Controllers
{
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _IngredientService;
        public IngredientController(IIngredientService IngredientService)
        {
            _IngredientService = IngredientService;
        }

        [HttpPost("GetFullFoodDataResults")]
        [OpenApiOperation("GetFullFoodDataResults")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<FoodDataVM>> GetFoodDataAsync(string search)
        {
            return await _IngredientService.GetFullFoodDataResultsAsync(search);
        }
    }
}
