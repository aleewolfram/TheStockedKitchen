using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TheStockedKitchen.Api.Services;
using TheStockedKitchen.Data.Model;
using TheStockedKitchen.Data.SpoonacularModel;

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
        public async Task<ActionResult<List<Recipe>>> GetRecipesAsync(string ingredients)
        {
            return await _RecipeService.GetRecipesAsync(ingredients);
        }
    }
}
