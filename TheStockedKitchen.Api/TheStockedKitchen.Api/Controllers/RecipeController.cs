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
        public async Task<ActionResult<RecipeDetailVM>> GetRecipeDetailAsync(RecipeVM recipeVM)
        {
            string user = User.Claims.Where(c => c.Type == "preferred_username").First().Value;
            return await _RecipeService.GetRecipeDetailAsync(recipeVM, user);
        }

        [HttpPost("MarkRecipeAsMade")]
        [OpenApiOperation("MarkRecipeAsMade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> MarkRecipeAsMadeAsync(RecipeVM recipeVM)
        {
            string user = User.Claims.Where(c => c.Type == "preferred_username").First().Value;
            return await _RecipeService.MarkRecipeAsMadeAsync(recipeVM, user);
        }

        [HttpPost("UndoMarkRecipeAsMade")]
        [OpenApiOperation("UndoMarkRecipeAsMade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> UndoMarkRecipeAsMadeAsync(int recipeId)
        {
            string user = User.Claims.Where(c => c.Type == "preferred_username").First().Value;
            return await _RecipeService.UndoMarkRecipeAsMadeAsync(recipeId, user);
        }
    }
}
