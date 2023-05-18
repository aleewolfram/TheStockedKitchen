using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStockedKitchen.Data.SpoonacularModel;

namespace TheStockedKitchen.Data.ViewModel
{
    public class IngredientVM
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public string UnitAbbreviation { get; set; }
        public string Image { get; set; }
        public double Quantity { get; set; }

        public IngredientVM()
        {

        }

        public IngredientVM(UsedIngredient usedIngredient)
        {
            Name = usedIngredient.name;
            Unit = usedIngredient.unitLong;
            UnitAbbreviation = usedIngredient.unitShort.ToUpper();
            Image = usedIngredient.image;
        }

        public IngredientVM(MissedIngredient missedIngredient)
        {
            Name = missedIngredient.name;
            Unit = missedIngredient.unitLong;
            UnitAbbreviation = missedIngredient.unitShort.ToUpper();
            Image = missedIngredient.image;
        }

        public IngredientVM(ExtendedIngredient extendedIngredient)
        {
            Name = extendedIngredient.name;
            Unit = extendedIngredient.unit;
            UnitAbbreviation = extendedIngredient.measures.us.unitShort.ToUpper();
            Image = extendedIngredient.image;
            Quantity = extendedIngredient.amount;
        }
    }
}
