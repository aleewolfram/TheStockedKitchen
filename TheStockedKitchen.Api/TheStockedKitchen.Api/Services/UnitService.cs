﻿using Microsoft.EntityFrameworkCore;
using TheStockedKitchen.Db;
using TheStockedKitchen.Data.Model;
using TheStockedKitchen.Data.ViewModel;
using TheStockedKitchen.Data.SpoonacularModel;
using System.Drawing;

namespace TheStockedKitchen.Api.Services
{
    public interface IUnitService
    {
        Task<List<FoodDataVM>> ApplyUnitInformationAsync(List<FoodDataVM> foodDataVMs);
        Task<List<Unit>> GetUnitsAsync();
        Task<IngredientCompareVM> GetPantryIngredientRemaining(IngredientCompareVM ingredientCompareVM);
        Task<string> GetUnitAbbreviationAsync(string unit);
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
            try
            {
                return await _dbContext.Unit.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving unit information.", ex);
            }
        }

        public async Task<List<FoodDataVM>> ApplyUnitInformationAsync(List<FoodDataVM> foodDataVMs)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("An error occurred while applying unit information.", ex);
            }
        }

        public async Task<string> GetUnitAbbreviationAsync(string unit)
        {
            try 
            {
                return await _dbContext.Unit.Where(u => u.Name == unit).Select(u => u.Abbreviation).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving unit abbreviations.", ex);
            }
        }

        public async Task<IngredientCompareVM> GetPantryIngredientRemaining(IngredientCompareVM ingredientCompareVM)
        {
            try
            {
                // Clean up conversion names if possible
                switch (ingredientCompareVM.RecipeIngredientUnitAbbreviation)
                {
                    case "CUP":
                        ingredientCompareVM.RecipeIngredientUnitAbbreviation = "C";
                        break;
                    case "CUPS":
                        ingredientCompareVM.RecipeIngredientUnitAbbreviation = "C";
                        break;
                    case "QTS":
                        ingredientCompareVM.RecipeIngredientUnitAbbreviation = "QT";
                        break;
                    case "TABLESPOONS":
                        ingredientCompareVM.RecipeIngredientUnitAbbreviation = "TBSP";
                        break;
                    case "TEASPOONS":
                        ingredientCompareVM.RecipeIngredientUnitAbbreviation = "TSP";
                        break;
                    case "OUNCES":
                        ingredientCompareVM.RecipeIngredientUnitAbbreviation = "OZ";
                        break;
                    default:
                        break;
                }

                UnitConversion unitConversion = await _dbContext.UnitConversion.Where(c => c.UnitAbbreviation == ingredientCompareVM.PantryIngredientUnitAbbreviation && c.CompareUnitAbbreviation == ingredientCompareVM.RecipeIngredientUnitAbbreviation).FirstOrDefaultAsync();

                if (unitConversion != null)
                {
                    // Get pantry ingredient in terms of recipe ingredient
                    double RecipeIngredientInTermsOfPantry = (ingredientCompareVM.RecipeIngredientQuantity * unitConversion.UnitAmount) / unitConversion.CompareUnitAmount;

                    ingredientCompareVM.PantryIngredientRemainingQuantity = Math.Round(ingredientCompareVM.PantryIngredientQuantity - RecipeIngredientInTermsOfPantry, 2);
                    ingredientCompareVM.PantryIngredientRemainingUnit = ingredientCompareVM.PantryIngredientUnit;
                    ingredientCompareVM.PantryIngredientRemainingUnitAbbreviation = ingredientCompareVM.PantryIngredientUnitAbbreviation;

                    ingredientCompareVM.WasAbleToCompare = true;
                }
                else
                {
                    ingredientCompareVM.PantryIngredientRemainingQuantity = 0;
                    ingredientCompareVM.PantryIngredientRemainingUnit = ingredientCompareVM.PantryIngredientUnit;
                    ingredientCompareVM.PantryIngredientRemainingUnitAbbreviation = ingredientCompareVM.PantryIngredientUnitAbbreviation;

                    ingredientCompareVM.WasAbleToCompare = false;
                }

                return ingredientCompareVM;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving pantry ingredients remaining.", ex);
            }
        }
    }
}
