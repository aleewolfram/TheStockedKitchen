using TheStockedKitchen.Data.USDANutrientModel;

namespace TheStockedKitchen.Data.ViewModel
{
    public class FoodStockVM
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public bool IncludedInRecipeSearch { get; set; }
    }
}
