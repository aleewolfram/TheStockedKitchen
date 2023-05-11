using TheStockedKitchen.Data.USDANutrientModel;

namespace TheStockedKitchen.Data.ViewModel
{
    public class FoodNutrientVM
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Value { get; set; }

        public FoodNutrientVM(FoodNutrient foodNutrient) 
        {
            Name = foodNutrient.nutrientName;
            Unit = foodNutrient.unitName;
            Value = foodNutrient.value;
        }
    }
}
