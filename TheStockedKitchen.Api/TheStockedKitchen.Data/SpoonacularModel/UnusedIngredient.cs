using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStockedKitchen.Data.SpoonacularModel
{
    public class UnusedIngredient
    {
        public string aisle { get; set; }
        public double amount { get; set; }
        public int id { get; set; }
        public string image { get; set; }
        public List<object> meta { get; set; }
        public string name { get; set; }
        public string original { get; set; }
        public string originalName { get; set; }
        public string unit { get; set; }
        public string unitLong { get; set; }
        public string unitShort { get; set; }
    }
}
