using TheStockedKitchen.Data.USDANutrientModel;

namespace TheStockedKitchen.Data.ViewModel
{
    public class FoodDataVM
    {
        public int FDCId { get; set; }
        public string Name { get; set; }
        public string FoodCategory { get; set; }
        public List<FoodNutrientVM> FoodNutrients { get; set; }
        public FoodDataVM(FoodData foodData) 
        {
            FDCId = foodData.fdcId;
            Name = foodData.description;
            FoodCategory = foodData.foodCategory;
            FoodNutrients = foodData.foodNutrients.Where(f => f.value != 0).Select(f => new FoodNutrientVM(f)).ToList();
        }
    }
}
