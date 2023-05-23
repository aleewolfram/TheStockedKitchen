using TheStockedKitchen.Api.Config;
using TheStockedKitchen.Api.Services;
using TheStockedKitchen.Data.ViewModel;

namespace TheStockedKitchen.Api.Utilities
{

    public class UnitConverter
    {
        private readonly IUnitService _unitService;

        public UnitConverter(IUnitService unitService)
        {
            _unitService = unitService;
        }

        public double GetUnitsInTermsOfFirstIngredient(IngredientVM firstIngredient, IngredientVM secondIngredient)
        {

            return 0;
        }
    }
}
