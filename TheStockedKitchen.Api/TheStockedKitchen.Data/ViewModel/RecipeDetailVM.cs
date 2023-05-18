using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStockedKitchen.Data.SpoonacularModel;

namespace TheStockedKitchen.Data.ViewModel
{
    public class RecipeDetailVM
    {
        public string SourceUrl { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Summary { get; set; }
        public List<IngredientVM> Ingredients { get; set; }

        public RecipeDetailVM(){}
        public RecipeDetailVM(RecipeDetail recipeDetail)
        {
            SourceUrl = recipeDetail.sourceUrl; 
            Title = recipeDetail.title; 
            Image = recipeDetail.image; 
            Summary = recipeDetail.summary;
            Ingredients = recipeDetail.extendedIngredients.Select(i => new IngredientVM(i)).ToList();
        }
    }
}
