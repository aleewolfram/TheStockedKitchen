using TheStockedKitchen.Data.USDANutrientModel;

namespace TheStockedKitchen.Data.ViewModel
{
    public class FoodNutrientVM
    {
        public string Name { get; set; }
        public string UnitAbbreviation { get; set; }
        public string UnitName { get; set; }
        public double Value { get; set; }

        public FoodNutrientVM()
        {

        }
        public FoodNutrientVM(FoodNutrient foodNutrient) 
        {
            Name = foodNutrient.nutrientName;
            UnitAbbreviation = foodNutrient.unitName;
            Value = foodNutrient.value;
        }
    }
}
