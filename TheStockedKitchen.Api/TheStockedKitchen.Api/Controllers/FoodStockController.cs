using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TheStockedKitchen.Api.Services;
using TheStockedKitchen.Data.Model;
using TheStockedKitchen.Data.ViewModel;

namespace SCGPlanningTool.Api.Controllers
{
    [Authorize]
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
            string user = User.Claims.Where(c => c.Type == "preferred_username").First().Value;
            return await _FoodStockService.GetFoodStockAsync(user);
        }

        [HttpPost("AddFoodStock")]
        [OpenApiOperation("AddFoodStock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> AddFoodStockAsync(FoodStockVM foodStockVM)
        {
            string user = User.Claims.Where(c => c.Type == "preferred_username").First().Value;
            return await _FoodStockService.AddFoodStockAsync(foodStockVM, user);
        }

        [HttpPost("DeleteFoodStock")]
        [OpenApiOperation("DeleteFoodStock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteFoodStockAsync(int foodStockId)
        {
            string user = User.Claims.Where(c => c.Type == "preferred_username").First().Value;
            return await _FoodStockService.DeleteFoodStockAsync(foodStockId, user);
        }

        [HttpPost("UpdateFoodStock")]
        [OpenApiOperation("UpdateFoodStock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> UpdateFoodStockAsync(FoodStock foodStock)
        {
            string user = User.Claims.Where(c => c.Type == "preferred_username").First().Value;
            return await _FoodStockService.UpdateFoodStockAsync(foodStock, user);
        }
    }
}
