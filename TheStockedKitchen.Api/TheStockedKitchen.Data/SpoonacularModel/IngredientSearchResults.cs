using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStockedKitchen.Data.SpoonacularModel
{
    public class IngredientSearchResults
    {
        public List<Results> results { get; set; }
        public int offset { get; set; }
        public int number { get; set; }
        public int totalResults { get; set; }
    }
}
