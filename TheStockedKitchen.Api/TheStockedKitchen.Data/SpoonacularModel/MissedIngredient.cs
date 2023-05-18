using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStockedKitchen.Data.SpoonacularModel
{
    public class MissedIngredient
    {
        public string aisle { get; set; }
        public double amount { get; set; }
        public int id { get; set; }
        public string image { get; set; }
        public List<string> meta { get; set; }
        public string name { get; set; }
        public string original { get; set; }
        public string originalName { get; set; }
        public string unit { get; set; }
        public string unitLong { get; set; }
        public string unitShort { get; set; }
        public string extendedName { get; set; }
    }
}
