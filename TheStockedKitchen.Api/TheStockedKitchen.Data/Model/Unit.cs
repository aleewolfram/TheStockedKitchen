using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStockedKitchen.Data.Model
{
    public class Unit
    {
        public int UnitId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public bool AllowedInDropDown { get; set; }

    }
}
