using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStockedKitchen.Data.ViewModel
{
    public class IngredientCompareVM
    {
        public string RecipeIngredientName { get; set; }
        public string RecipeIngredientUnit { get; set; }
        public string RecipeIngredientUnitAbbreviation { get; set; }
        public double RecipeIngredientQuantity { get; set; }
        public string PantryIngredientName { get; set; }
        public string PantryIngredientUnit { get; set; }
        public string PantryIngredientUnitAbbreviation { get; set; }
        public double PantryIngredientQuantity { get; set; }
        public string PantryIngredientRemainingUnit { get; set; }
        public string PantryIngredientUnitRemainingAbbreviation { get; set; }
        public double PantryIngredientRemainingQuantity { get; set; }
        public bool WasAbleToCompare { get; set; }
    }
}
