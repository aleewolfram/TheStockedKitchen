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
        Task<bool> ToggleFoodStockIncludedInRecipeAsync(int foodStockId, string user);
        Task<bool> UpdateFoodStockQuantityAsync(List<IngredientCompareVM> ingredientCompareVMs);
    }
    public class FoodStockService : IFoodStockService
    {

        private readonly AppDBContext _dbContext;
        private readonly IUnitService _unitService;

        public FoodStockService(AppDBContext dbContext, IUnitService unitService)
        {
            _dbContext = dbContext;
            _unitService = unitService;
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
                    UnitAbbreviation = await _unitService.GetUnitAbbreviationAsync(foodStockVM.Unit),
                    Quantity = foodStockVM.Quantity,
                    User = user,
                    Image = foodStockVM.Image,
                    IncludedInRecipeSearch = true,
                    Category = foodStockVM.Category,
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
                    updateFoodStock.UnitAbbreviation = await _unitService.GetUnitAbbreviationAsync(foodStock.Unit);
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
        
        public async Task<bool> ToggleFoodStockIncludedInRecipeAsync(int foodStockId, string user)
        {
            try
            {
                FoodStock updateFoodStock = await _dbContext.FoodStock.Where(f => f.User == user && f.FoodStockId == foodStockId).SingleAsync();

                if (updateFoodStock != null)
                {
                    updateFoodStock.IncludedInRecipeSearch = !updateFoodStock.IncludedInRecipeSearch;

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

        public async Task<bool> UpdateFoodStockQuantityAsync(List<IngredientCompareVM> ingredientCompareVMs)
        {
            try
            {
                foreach(IngredientCompareVM ingredientCompareVM in ingredientCompareVMs)
                {
                    FoodStock updateFoodStock = await _dbContext.FoodStock.Where(f => f.FoodStockId == ingredientCompareVM.PantryIngredientId).SingleAsync();
                    if (updateFoodStock != null)
                    {
                        updateFoodStock.Quantity = ingredientCompareVM.PantryIngredientRemainingQuantity;
                        updateFoodStock.Unit = ingredientCompareVM.PantryIngredientRemainingUnit;
                        updateFoodStock.UnitAbbreviation = ingredientCompareVM.PantryIngredientRemainingUnitAbbreviation;
                        updateFoodStock.LastEditedDate = DateTime.Now;
                    }
                    else
                    {
                        return false;
                    }
                }

                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }
        }
    }
}
