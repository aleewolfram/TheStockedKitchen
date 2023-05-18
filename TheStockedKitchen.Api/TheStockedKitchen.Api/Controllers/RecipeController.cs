using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TheStockedKitchen.Api.Services;
using TheStockedKitchen.Data.ViewModel;

namespace SCGPlanningTool.Api.Controllers
{
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _RecipeService;
        public RecipeController(IRecipeService RecipeService)
        {
            _RecipeService = RecipeService;
        }

        [HttpPost("GetRecipes")]
        [OpenApiOperation("GetRecipes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<RecipeVM>>> GetRecipesAsync(string ingredients)
        {
            return await _RecipeService.GetRecipesAsync(ingredients);
        }

        [HttpPost("GetRecipeDetail")]
        [OpenApiOperation("GetRecipeDetail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RecipeDetailVM>> GetRecipeDetailAsync(int id)
        {
            return await _RecipeService.GetRecipeDetailAsync(id);
        }
    }
}
