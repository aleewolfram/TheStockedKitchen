using TheStockedKitchen.Data.Model;
using TheStockedKitchen.Data.SpoonacularModel;

namespace TheStockedKitchen.Data.ViewModel
{
    public class RecipeDetailVM
    {
        public string SourceUrl { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Summary { get; set; }
        public List<IngredientCompareVM> IngredientCompareVMs { get; set; }
        public List<string> Instructions { get; set; }
    }
}
