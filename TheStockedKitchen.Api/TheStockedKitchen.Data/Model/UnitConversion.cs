using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStockedKitchen.Data.Model
{
    public class UnitConversion
    {
        public int UnitConversionId { get; set; }
        public string UnitAbbreviation { get; set; }
        public double UnitAmount { get; set; }
        public string CompareUnitAbbreviation { get; set; }
        public double CompareUnitAmount { get; set; }

    }
}
