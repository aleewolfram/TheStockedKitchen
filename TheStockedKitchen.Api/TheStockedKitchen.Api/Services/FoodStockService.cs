using Microsoft.EntityFrameworkCore;
using TheStockedKitchen.Db;
using TheStockedKitchen.Data.Model;
using TheStockedKitchen.Data.ViewModel;

namespace TheStockedKitchen.Api.Services
{
    public interface IFoodStockService
    {
        Task<List<FoodStock>> GetFoodStockAsync(string user);
        Task<bool> AddFoodStockAsync(FoodStockVM foodStockVM, string user);
        Task<bool> DeleteFoodStockAsync(int foodStockId, string user);
        Task<bool> UpdateFoodStockAsync(FoodStock foodStock, string user);
    }
    public class FoodStockService : IFoodStockService
    {

        private readonly AppDBContext _dbContext;

        public FoodStockService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<FoodStock>> GetFoodStockAsync(string user)
        {
            return await _dbContext.FoodStock.Where(f => f.User == user).ToListAsync();
        }

        public async Task<bool> AddFoodStockAsync(FoodStockVM foodStockVM, string user)
        {
            try
            {
                FoodStock foodStock = new FoodStock
                {
                    Name = foodStockVM.Name,
                    Unit = foodStockVM.Unit,
                    Quantity = foodStockVM.Quantity,
                    User = user,
                    CreatedDate = DateTime.Now,
                    LastEditedDate = DateTime.Now
                };

                await _dbContext.FoodStock.AddAsync(foodStock);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<bool> DeleteFoodStockAsync(int foodStockId, string user)
        {
            try
            {
                FoodStock foodStock = await _dbContext.FoodStock.Where(f => f.User == user && f.FoodStockId == foodStockId).SingleAsync();

                _dbContext.FoodStock.Remove(foodStock);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<bool> UpdateFoodStockAsync(FoodStock foodStock, string user)
        {
            try
            {
                FoodStock updateFoodStock = await _dbContext.FoodStock.Where(f => f.User == user && f.FoodStockId == foodStock.FoodStockId).SingleAsync();

                if(updateFoodStock != null)
                {
                    updateFoodStock.Quantity = foodStock.Quantity;
                    updateFoodStock.Unit = foodStock.Unit;
                    updateFoodStock.LastEditedDate = DateTime.Now; 
                    
                    await _dbContext.SaveChangesAsync();

                    return true;

                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
