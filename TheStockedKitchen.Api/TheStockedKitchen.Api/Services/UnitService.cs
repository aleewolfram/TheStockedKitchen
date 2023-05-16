using Microsoft.EntityFrameworkCore;
using TheStockedKitchen.Db;
using TheStockedKitchen.Data.Model;
using TheStockedKitchen.Data.ViewModel;

namespace TheStockedKitchen.Api.Services
{
    public interface IUnitService
    {
        Task<List<FoodDataVM>> ApplyUnitInformationAsync(List<FoodDataVM> foodDataVMs);
        Task<List<Unit>> GetUnitsAsync();
    }
    public class UnitService : IUnitService
    {

        private readonly AppDBContext _dbContext;

        public UnitService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Unit>> GetUnitsAsync()
        {
            return await _dbContext.Unit.ToListAsync();
        }

        public async Task<List<FoodDataVM>> ApplyUnitInformationAsync(List<FoodDataVM> foodDataVMs)
        {
            List<Unit> units = await GetUnitsAsync();
            Dictionary<string, string> unitDictionary = units.ToDictionary(u => u.Abbreviation, u => u.Name);

            foreach (FoodDataVM foodDataVM in foodDataVMs)
            {
                foreach (FoodNutrientVM foodNutrientVM in foodDataVM.FoodNutrients)
                {
                    string unitAbbreviation = foodNutrientVM.UnitAbbreviation;
                    if (unitDictionary.ContainsKey(unitAbbreviation))
                    {
                        foodNutrientVM.UnitName = unitDictionary[unitAbbreviation];
                    }
                }
            }

            return foodDataVMs;
        }
    }
}
