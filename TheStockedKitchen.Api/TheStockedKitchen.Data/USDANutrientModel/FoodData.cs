using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStockedKitchen.Data.USDANutrientModel
{
    public class FoodData
    {
        public int fdcId { get; set; }
        public string description { get; set; }
        public string commonNames { get; set; }
        public string additionalDescriptions { get; set; }
        public string dataType { get; set; }
        public int ndbNumber { get; set; }
        public string publishedDate { get; set; }
        public string foodCategory { get; set; }
        public string allHighlightFields { get; set; }
        public double score { get; set; }
        public List<object> microbes { get; set; }
        public List<FoodNutrient> foodNutrients { get; set; }
        public List<object> finalFoodInputFoods { get; set; }
        public List<object> foodMeasures { get; set; }
        public List<object> foodAttributes { get; set; }
        public List<object> foodAttributeTypes { get; set; }
        public List<object> foodVersionIds { get; set; }
    }
}
