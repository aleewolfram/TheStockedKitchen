using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStockedKitchen.Data.SpoonacularModel;

namespace TheStockedKitchen.Data.ViewModel
{
    public class RecipeVM
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Likes { get; set; }
        public int MissingIngredientsCount { get; set; }

        public List<IngredientVM> MissingIngredients { get; set; }

        public List<IngredientVM> UsedIngredients { get; set; }

        public RecipeVM() { }

        public RecipeVM(Recipe recipe)
        {
            RecipeId = recipe.id;
            Title = recipe.title;
            Image = recipe.image;
            Likes = recipe.likes;
            MissingIngredientsCount = recipe.missedIngredientCount;
            MissingIngredients = recipe.missedIngredients.Select(r => new IngredientVM(r)).ToList();
            UsedIngredients = recipe.usedIngredients.Select(r => new IngredientVM(r)).ToList();
        }
    }
}
