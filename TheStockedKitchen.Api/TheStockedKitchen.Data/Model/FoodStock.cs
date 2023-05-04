namespace TheStockedKitchen.Data.Model
{
    public class FoodStock
    {
        public int FoodStockId { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public string User { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastEditedDate { get; set; }
    }
}