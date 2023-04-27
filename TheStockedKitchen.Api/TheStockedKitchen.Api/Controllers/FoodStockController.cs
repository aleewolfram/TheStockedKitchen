using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using SCGPlanningTool.Api.Services;
using TheStockedKitchen.Data.Model;

namespace SCGPlanningTool.Api.Controllers
{
    [ApiController]
    public class FoodStockController : ControllerBase
    {
        private readonly IFoodStockService _FoodStockService;
        public FoodStockController(IFoodStockService FoodStockService)
        {
            _FoodStockService = FoodStockService;
        }

        [HttpGet("GetFoodStock")]
        [OpenApiOperation("GetFoodStock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FoodStock>>> GetFoodStockAsync()
        {
            return await _FoodStockService.GetFoodStockAsync();
        }
    }
}
