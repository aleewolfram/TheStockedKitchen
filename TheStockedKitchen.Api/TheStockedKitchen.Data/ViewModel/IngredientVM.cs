﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStockedKitchen.Data.ViewModel
{
    public class IngredientVM
    {
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Unit { get; set; }
        public string UnitAbbreviation { get; set; }
        public string Image { get; set; }
        public double Quantity { get; set; }
    }
}
