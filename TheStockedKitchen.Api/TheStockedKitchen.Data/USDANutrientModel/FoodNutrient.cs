using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStockedKitchen.Data.USDANutrientModel
{
    public class FoodNutrient
    {
        public int nutrientId { get; set; }
        public string nutrientName { get; set; }
        public string nutrientNumber { get; set; }
        public string unitName { get; set; }
        public string derivationCode { get; set; }
        public string derivationDescription { get; set; }
        public int derivationId { get; set; }
        public double value { get; set; }
        public int foodNutrientSourceId { get; set; }
        public string foodNutrientSourceCode { get; set; }
        public string foodNutrientSourceDescription { get; set; }
        public int rank { get; set; }
        public int indentLevel { get; set; }
        public int foodNutrientId { get; set; }
        public int dataPoints { get; set; }
    }
}
