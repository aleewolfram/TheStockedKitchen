namespace TheStockedKitchen.Data.Model
{
    public class RecipeMade
    {
        public int RecipeMadeId { get; set; }
        public int SpoonacularRecipeId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string User { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}