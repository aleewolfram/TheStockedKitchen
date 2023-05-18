using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStockedKitchen.Data.SpoonacularModel
{
    public class Recipe
    {
        public int id { get; set; }
        public string image { get; set; }
        public string imageType { get; set; }
        public int likes { get; set; }
        public int missedIngredientCount { get; set; }
        public List<MissedIngredient> missedIngredients { get; set; }
        public string title { get; set; }
        public List<UnusedIngredient> unusedIngredients { get; set; }
        public int usedIngredientCount { get; set; }
        public List<UsedIngredient> usedIngredients { get; set; }
    }
}
