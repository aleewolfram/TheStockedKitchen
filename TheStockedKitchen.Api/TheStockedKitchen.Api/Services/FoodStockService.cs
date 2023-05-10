using Microsoft.EntityFrameworkCore;
using TheStockedKitchen.Db;
using TheStockedKitchen.Data.Model;

namespace TheStockedKitchen.Api.Services
{
    public interface IFoodStockService
    {
        Task<List<FoodStock>> GetFoodStockAsync();
    }
    public class FoodStockService : IFoodStockService
    {

        private readonly AppDBContext _dbContext;

        public FoodStockService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<FoodStock>> GetFoodStockAsync()
        {
            return await _dbContext.FoodStock.ToListAsync();
        }
    }
}
