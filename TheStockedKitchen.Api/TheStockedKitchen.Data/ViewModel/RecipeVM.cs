using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStockedKitchen.Data.ViewModel
{
    internal class RecipeVM
    {
        public int RecipeId { get; set; }

        public string Image { get; set; }

        public int Likes { get; set; }

        public int MissingIngredientsCount { get; set; }

        public List<IngredientVM> MissingIngredients { get; set; }

        public List<IngredientVM> UsedIngredients { get; set; }
    }
}
