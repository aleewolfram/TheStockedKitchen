using TheStockedKitchen.Data.SpoonacularModel;
using TheStockedKitchen.Data.USDANutrientModel;

namespace TheStockedKitchen.Data.ViewModel
{
    public class FoodDataVM
    {
        public int FDCId { get; set; }
        public int NBDNumber { get; set; }
        public string Name { get; set; }
        public string FoodCategory { get; set; }
        public string Image { get; set; }
        public List<FoodNutrientVM> FoodNutrients { get; set; }

        public FoodDataVM()
        {
        }
        public FoodDataVM(FoodData foodData) 
        {
            FDCId = foodData.fdcId;
            NBDNumber = foodData.ndbNumber;
            Name = foodData.description;
            FoodCategory = foodData.foodCategory;
            FoodNutrients = foodData.foodNutrients.Where(f => f.value != 0).Select(f => new FoodNutrientVM(f)).ToList();
        }

        public FoodDataVM(Results results)
        {
            NBDNumber = results.id;
            Name = results.name;
            Image = "https://spoonacular.com/cdn/ingredients_100x100/" + results.image;
        }
    }
}
