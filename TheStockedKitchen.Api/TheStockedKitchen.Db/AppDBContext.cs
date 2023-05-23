using Microsoft.EntityFrameworkCore;
using TheStockedKitchen.Data.Model;

namespace TheStockedKitchen.Db
{
    public partial class AppDBContext : DbContext
    {
        public AppDBContext()
        {
        }

        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<FoodStock> FoodStock { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<UnitConversion> UnitConversion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodStock>(entity =>
            {
                entity.HasKey(e => e.FoodStockId);

                entity.ToTable("FoodStock", "dbo");

                entity.Property(e => e.FoodStockId).HasColumnName("FoodStockId");

                entity.Property(e => e.Name).HasColumnName("Name");

                entity.Property(e => e.Quantity).HasColumnName("Quantity");

                entity.Property(e => e.Unit).HasColumnName("Unit");

                entity.Property(e => e.UnitAbbreviation).HasColumnName("UnitAbbreviation");

                entity.Property(e => e.Image).HasColumnName("Image");

                entity.Property(e => e.Category).HasColumnName("Category");

                entity.Property(e => e.IncludedInRecipeSearch).HasColumnName("IncludedInRecipeSearch");

                entity.Property(e => e.User).HasColumnName("User");

                entity.Property(e => e.CreatedDate).HasColumnName("CreatedDate");

                entity.Property(e => e.LastEditedDate).HasColumnName("LastEditedDate");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
            entity.HasKey(e => e.UnitId);

                entity.ToTable("Unit", "dbo");

                entity.Property(e => e.UnitId).HasColumnName("UnitId");

                entity.Property(e => e.Name).HasColumnName("Name");

                entity.Property(e => e.Abbreviation).HasColumnName("Abbreviation");

                entity.Property(e => e.AllowedInDropDown).HasColumnName("AllowedInDropDown");
            });

            modelBuilder.Entity<Unit>().HasData(
                new Unit { UnitId = 1, Name = "Milligrams", Abbreviation = "MG", AllowedInDropDown = false },
                new Unit { UnitId = 2, Name = "Micrograms", Abbreviation = "UG", AllowedInDropDown = false },
                new Unit { UnitId = 3, Name = "Grams", Abbreviation = "G", AllowedInDropDown = true },
                new Unit { UnitId = 4, Name = "International Units", Abbreviation = "IU", AllowedInDropDown = false },
                new Unit { UnitId = 5, Name = "Kilojoules", Abbreviation = "kJ", AllowedInDropDown = false },
                new Unit { UnitId = 6, Name = "Calories", Abbreviation = "KCAL", AllowedInDropDown = false },
                new Unit { UnitId = 7, Name = "Ounces", Abbreviation = "OZ", AllowedInDropDown = true },
                new Unit { UnitId = 8, Name = "Cups", Abbreviation = "C", AllowedInDropDown = true },
                new Unit { UnitId = 9, Name = "Pounds", Abbreviation = "LB", AllowedInDropDown = true },
                new Unit { UnitId = 10, Name = "Tablespoons", Abbreviation = "TBSP", AllowedInDropDown = true },
                new Unit { UnitId = 11, Name = "Teaspoons", Abbreviation = "TSP", AllowedInDropDown = true },
                new Unit { UnitId = 12, Name = "Fluid Ounces", Abbreviation = "FL OZ", AllowedInDropDown = true },
                new Unit { UnitId = 13, Name = "Pints", Abbreviation = "PT", AllowedInDropDown = true },
                new Unit { UnitId = 14, Name = "Quarts", Abbreviation = "QT", AllowedInDropDown = true },
                new Unit { UnitId = 15, Name = "Gallons", Abbreviation = "GAL", AllowedInDropDown = true },
                new Unit { UnitId = 16, Name = "Whole", Abbreviation = "W", AllowedInDropDown = true }

            );

            modelBuilder.Entity<UnitConversion>(entity =>
            {
                entity.HasKey(e => e.UnitConversionId);

                entity.ToTable("UnitConversion", "dbo");

                entity.Property(e => e.UnitConversionId).HasColumnName("UnitConversionId");

                entity.Property(e => e.UnitAbbreviation).HasColumnName("UnitAbbreviation");

                entity.Property(e => e.UnitAmount).HasColumnName("UnitAmount");

                entity.Property(e => e.CompareUnitAbbreviation).HasColumnName("CompareUnitAbbreviation");

                entity.Property(e => e.CompareUnitAmount).HasColumnName("CompareUnitAmount");
            });

            modelBuilder.Entity<UnitConversion>().HasData(
                new UnitConversion { UnitConversionId = 1, UnitAbbreviation = "G", UnitAmount = 1, CompareUnitAbbreviation = "G", CompareUnitAmount = 1 },
                new UnitConversion { UnitConversionId = 2, UnitAbbreviation = "G", UnitAmount = 1, CompareUnitAbbreviation = "OZ", CompareUnitAmount = 0.035274 },
                new UnitConversion { UnitConversionId = 3, UnitAbbreviation = "G", UnitAmount = 1, CompareUnitAbbreviation = "C", CompareUnitAmount = 0.004226752838 },
                new UnitConversion { UnitConversionId = 4, UnitAbbreviation = "G", UnitAmount = 1, CompareUnitAbbreviation = "LB", CompareUnitAmount = 0.00220462 },
                new UnitConversion { UnitConversionId = 5, UnitAbbreviation = "G", UnitAmount = 1, CompareUnitAbbreviation = "TBSP", CompareUnitAmount = 0.0676280454 },
                new UnitConversion { UnitConversionId = 6, UnitAbbreviation = "G", UnitAmount = 1, CompareUnitAbbreviation = "TSP", CompareUnitAmount = 0.2028841362 },
                new UnitConversion { UnitConversionId = 7, UnitAbbreviation = "G", UnitAmount = 1, CompareUnitAbbreviation = "FL OZ", CompareUnitAmount = 29.57 },
                new UnitConversion { UnitConversionId = 8, UnitAbbreviation = "G", UnitAmount = 1, CompareUnitAbbreviation = "PT", CompareUnitAmount = 0.002113376419 },
                new UnitConversion { UnitConversionId = 9, UnitAbbreviation = "G", UnitAmount = 1, CompareUnitAbbreviation = "QT", CompareUnitAmount = 0.001056688209 },
                new UnitConversion { UnitConversionId = 10, UnitAbbreviation = "G", UnitAmount = 1, CompareUnitAbbreviation = "GAL", CompareUnitAmount = 0.000264172052 },
                new UnitConversion { UnitConversionId = 11, UnitAbbreviation = "OZ", UnitAmount = 1, CompareUnitAbbreviation = "G", CompareUnitAmount = 28.3495 },
                new UnitConversion { UnitConversionId = 12, UnitAbbreviation = "OZ", UnitAmount = 1, CompareUnitAbbreviation = "OZ", CompareUnitAmount = 1 },
                new UnitConversion { UnitConversionId = 13, UnitAbbreviation = "OZ", UnitAmount = 1, CompareUnitAbbreviation = "C", CompareUnitAmount = 0.119826427325 },
                new UnitConversion { UnitConversionId = 14, UnitAbbreviation = "OZ", UnitAmount = 1, CompareUnitAbbreviation = "LB", CompareUnitAmount = 0.0625 },
                new UnitConversion { UnitConversionId = 15, UnitAbbreviation = "OZ", UnitAmount = 1, CompareUnitAbbreviation = "TBSP", CompareUnitAmount = 2 },
                new UnitConversion { UnitConversionId = 16, UnitAbbreviation = "OZ", UnitAmount = 1, CompareUnitAbbreviation = "TSP", CompareUnitAmount = 6 },
                new UnitConversion { UnitConversionId = 17, UnitAbbreviation = "OZ", UnitAmount = 1, CompareUnitAbbreviation = "FL OZ", CompareUnitAmount = 0.958611418535 },
                new UnitConversion { UnitConversionId = 18, UnitAbbreviation = "OZ", UnitAmount = 1, CompareUnitAbbreviation = "PT", CompareUnitAmount = 0.0520421 },
                new UnitConversion { UnitConversionId = 19, UnitAbbreviation = "OZ", UnitAmount = 1, CompareUnitAbbreviation = "QT", CompareUnitAmount = 0.03125 },
                new UnitConversion { UnitConversionId = 20, UnitAbbreviation = "OZ", UnitAmount = 1, CompareUnitAbbreviation = "GAL", CompareUnitAmount = 0.0078125 },
                new UnitConversion { UnitConversionId = 21, UnitAbbreviation = "C", UnitAmount = 1, CompareUnitAbbreviation = "G", CompareUnitAmount = 236.58823648491 },
                new UnitConversion { UnitConversionId = 22, UnitAbbreviation = "C", UnitAmount = 1, CompareUnitAbbreviation = "OZ", CompareUnitAmount = 8.345404451487 },
                new UnitConversion { UnitConversionId = 23, UnitAbbreviation = "C", UnitAmount = 1, CompareUnitAbbreviation = "C", CompareUnitAmount = 1 },
                new UnitConversion { UnitConversionId = 24, UnitAbbreviation = "C", UnitAmount = 1, CompareUnitAbbreviation = "LB", CompareUnitAmount = 0.521587778218 },
                new UnitConversion { UnitConversionId = 25, UnitAbbreviation = "C", UnitAmount = 1, CompareUnitAbbreviation = "TBSP", CompareUnitAmount = 16 },
                new UnitConversion { UnitConversionId = 26, UnitAbbreviation = "C", UnitAmount = 1, CompareUnitAbbreviation = "TSP", CompareUnitAmount = 48 },
                new UnitConversion { UnitConversionId = 27, UnitAbbreviation = "C", UnitAmount = 1, CompareUnitAbbreviation = "FL OZ", CompareUnitAmount = 8 },
                new UnitConversion { UnitConversionId = 28, UnitAbbreviation = "C", UnitAmount = 1, CompareUnitAbbreviation = "PT", CompareUnitAmount = 0.5 },
                new UnitConversion { UnitConversionId = 29, UnitAbbreviation = "C", UnitAmount = 1, CompareUnitAbbreviation = "QT", CompareUnitAmount = 0.25 },
                new UnitConversion { UnitConversionId = 30, UnitAbbreviation = "C", UnitAmount = 1, CompareUnitAbbreviation = "GAL", CompareUnitAmount = 0.0625 },
                new UnitConversion { UnitConversionId = 31, UnitAbbreviation = "LB", UnitAmount = 1, CompareUnitAbbreviation = "G", CompareUnitAmount = 453.59237 },
                new UnitConversion { UnitConversionId = 32, UnitAbbreviation = "LB", UnitAmount = 1, CompareUnitAbbreviation = "OZ", CompareUnitAmount = 16 },
                new UnitConversion { UnitConversionId = 33, UnitAbbreviation = "LB", UnitAmount = 1, CompareUnitAbbreviation = "C", CompareUnitAmount = 1.917222837193 },
                new UnitConversion { UnitConversionId = 34, UnitAbbreviation = "LB", UnitAmount = 1, CompareUnitAbbreviation = "LB", CompareUnitAmount = 1 },
                new UnitConversion { UnitConversionId = 35, UnitAbbreviation = "LB", UnitAmount = 1, CompareUnitAbbreviation = "TBSP", CompareUnitAmount = 30.675565391454 },
                new UnitConversion { UnitConversionId = 36, UnitAbbreviation = "LB", UnitAmount = 1, CompareUnitAbbreviation = "TSP", CompareUnitAmount = 92.026696174362 },
                new UnitConversion { UnitConversionId = 37, UnitAbbreviation = "LB", UnitAmount = 1, CompareUnitAbbreviation = "FL OZ", CompareUnitAmount = 15.337782696563 },
                new UnitConversion { UnitConversionId = 38, UnitAbbreviation = "LB", UnitAmount = 1, CompareUnitAbbreviation = "PT", CompareUnitAmount = 0.958611418535 },
                new UnitConversion { UnitConversionId = 39, UnitAbbreviation = "LB", UnitAmount = 1, CompareUnitAbbreviation = "QT", CompareUnitAmount = 0.479305709268 },
                new UnitConversion { UnitConversionId = 40, UnitAbbreviation = "LB", UnitAmount = 1, CompareUnitAbbreviation = "GAL", CompareUnitAmount = 0.119826427317 },
                new UnitConversion { UnitConversionId = 41, UnitAbbreviation = "TBSP", UnitAmount = 1, CompareUnitAbbreviation = "G", CompareUnitAmount = 14.786764782056 },
                new UnitConversion { UnitConversionId = 42, UnitAbbreviation = "TBSP", UnitAmount = 1, CompareUnitAbbreviation = "OZ", CompareUnitAmount = 0.52158777828 },
                new UnitConversion { UnitConversionId = 43, UnitAbbreviation = "TBSP", UnitAmount = 1, CompareUnitAbbreviation = "C", CompareUnitAmount = 0.0625 },
                new UnitConversion { UnitConversionId = 44, UnitAbbreviation = "TBSP", UnitAmount = 1, CompareUnitAbbreviation = "LB", CompareUnitAmount = 0.032599236142 },
                new UnitConversion { UnitConversionId = 45, UnitAbbreviation = "TBSP", UnitAmount = 1, CompareUnitAbbreviation = "TBSP", CompareUnitAmount = 1 },
                new UnitConversion { UnitConversionId = 46, UnitAbbreviation = "TBSP", UnitAmount = 1, CompareUnitAbbreviation = "TSP", CompareUnitAmount = 3 },
                new UnitConversion { UnitConversionId = 47, UnitAbbreviation = "TBSP", UnitAmount = 1, CompareUnitAbbreviation = "FL OZ", CompareUnitAmount = 0.5 },
                new UnitConversion { UnitConversionId = 48, UnitAbbreviation = "TBSP", UnitAmount = 1, CompareUnitAbbreviation = "PT", CompareUnitAmount = 0.03125 },
                new UnitConversion { UnitConversionId = 49, UnitAbbreviation = "TBSP", UnitAmount = 1, CompareUnitAbbreviation = "QT", CompareUnitAmount = 0.015625 },
                new UnitConversion { UnitConversionId = 50, UnitAbbreviation = "TBSP", UnitAmount = 1, CompareUnitAbbreviation = "GAL", CompareUnitAmount = 0.003906 },
                new UnitConversion { UnitConversionId = 51, UnitAbbreviation = "TSP", UnitAmount = 1, CompareUnitAbbreviation = "G", CompareUnitAmount = 4.928921594019 },
                new UnitConversion { UnitConversionId = 52, UnitAbbreviation = "TSP", UnitAmount = 1, CompareUnitAbbreviation = "OZ", CompareUnitAmount = 0.17386259276 },
                new UnitConversion { UnitConversionId = 53, UnitAbbreviation = "TSP", UnitAmount = 1, CompareUnitAbbreviation = "C", CompareUnitAmount = 0.020833 },
                new UnitConversion { UnitConversionId = 54, UnitAbbreviation = "TSP", UnitAmount = 1, CompareUnitAbbreviation = "LB", CompareUnitAmount = 0.010866412047 },
                new UnitConversion { UnitConversionId = 55, UnitAbbreviation = "TSP", UnitAmount = 1, CompareUnitAbbreviation = "TBSP", CompareUnitAmount = 0.333333 },
                new UnitConversion { UnitConversionId = 56, UnitAbbreviation = "TSP", UnitAmount = 1, CompareUnitAbbreviation = "TSP", CompareUnitAmount = 1 },
                new UnitConversion { UnitConversionId = 57, UnitAbbreviation = "TSP", UnitAmount = 1, CompareUnitAbbreviation = "FL OZ", CompareUnitAmount = 0.166667 },
                new UnitConversion { UnitConversionId = 58, UnitAbbreviation = "TSP", UnitAmount = 1, CompareUnitAbbreviation = "PT", CompareUnitAmount = 0.010417 },
                new UnitConversion { UnitConversionId = 59, UnitAbbreviation = "TSP", UnitAmount = 1, CompareUnitAbbreviation = "QT", CompareUnitAmount = 0.005208 },
                new UnitConversion { UnitConversionId = 60, UnitAbbreviation = "TSP", UnitAmount = 1, CompareUnitAbbreviation = "GAL", CompareUnitAmount = 0.001302 },
                new UnitConversion { UnitConversionId = 61, UnitAbbreviation = "FL OZ", UnitAmount = 1, CompareUnitAbbreviation = "G", CompareUnitAmount = 29.5735295625 },
                new UnitConversion { UnitConversionId = 62, UnitAbbreviation = "FL OZ", UnitAmount = 1, CompareUnitAbbreviation = "OZ", CompareUnitAmount = 1.043175556502 },
                new UnitConversion { UnitConversionId = 63, UnitAbbreviation = "FL OZ", UnitAmount = 1, CompareUnitAbbreviation = "C", CompareUnitAmount = 0.125 },
                new UnitConversion { UnitConversionId = 64, UnitAbbreviation = "FL OZ", UnitAmount = 1, CompareUnitAbbreviation = "LB", CompareUnitAmount = 0.065198472281 },
                new UnitConversion { UnitConversionId = 65, UnitAbbreviation = "FL OZ", UnitAmount = 1, CompareUnitAbbreviation = "TBSP", CompareUnitAmount = 2 },
                new UnitConversion { UnitConversionId = 66, UnitAbbreviation = "FL OZ", UnitAmount = 1, CompareUnitAbbreviation = "TSP", CompareUnitAmount = 6 },
                new UnitConversion { UnitConversionId = 67, UnitAbbreviation = "FL OZ", UnitAmount = 1, CompareUnitAbbreviation = "FL OZ", CompareUnitAmount = 1 },
                new UnitConversion { UnitConversionId = 68, UnitAbbreviation = "FL OZ", UnitAmount = 1, CompareUnitAbbreviation = "PT", CompareUnitAmount = 0.0625 },
                new UnitConversion { UnitConversionId = 69, UnitAbbreviation = "FL OZ", UnitAmount = 1, CompareUnitAbbreviation = "QT", CompareUnitAmount = 0.03125 },
                new UnitConversion { UnitConversionId = 70, UnitAbbreviation = "FL OZ", UnitAmount = 1, CompareUnitAbbreviation = "GAL", CompareUnitAmount = 0.007812 },
                new UnitConversion { UnitConversionId = 71, UnitAbbreviation = "PT", UnitAmount = 1, CompareUnitAbbreviation = "G", CompareUnitAmount = 473.176473 },
                new UnitConversion { UnitConversionId = 72, UnitAbbreviation = "PT", UnitAmount = 1, CompareUnitAbbreviation = "OZ", CompareUnitAmount = 16.690808904039 },
                new UnitConversion { UnitConversionId = 73, UnitAbbreviation = "PT", UnitAmount = 1, CompareUnitAbbreviation = "C", CompareUnitAmount = 2 },
                new UnitConversion { UnitConversionId = 74, UnitAbbreviation = "PT", UnitAmount = 1, CompareUnitAbbreviation = "LB", CompareUnitAmount = 1.043175556502 },
                new UnitConversion { UnitConversionId = 75, UnitAbbreviation = "PT", UnitAmount = 1, CompareUnitAbbreviation = "TBSP", CompareUnitAmount = 32 },
                new UnitConversion { UnitConversionId = 76, UnitAbbreviation = "PT", UnitAmount = 1, CompareUnitAbbreviation = "TSP", CompareUnitAmount = 96 },
                new UnitConversion { UnitConversionId = 77, UnitAbbreviation = "PT", UnitAmount = 1, CompareUnitAbbreviation = "FL OZ", CompareUnitAmount = 16 },
                new UnitConversion { UnitConversionId = 78, UnitAbbreviation = "PT", UnitAmount = 1, CompareUnitAbbreviation = "PT", CompareUnitAmount = 1 },
                new UnitConversion { UnitConversionId = 79, UnitAbbreviation = "PT", UnitAmount = 1, CompareUnitAbbreviation = "QT", CompareUnitAmount = 0.5 },
                new UnitConversion { UnitConversionId = 80, UnitAbbreviation = "PT", UnitAmount = 1, CompareUnitAbbreviation = "GAL", CompareUnitAmount = 0.125 },
                new UnitConversion { UnitConversionId = 81, UnitAbbreviation = "QT", UnitAmount = 1, CompareUnitAbbreviation = "G", CompareUnitAmount = 946.35294599999 },
                new UnitConversion { UnitConversionId = 82, UnitAbbreviation = "QT", UnitAmount = 1, CompareUnitAbbreviation = "OZ", CompareUnitAmount = 33.381617808077 },
                new UnitConversion { UnitConversionId = 83, UnitAbbreviation = "QT", UnitAmount = 1, CompareUnitAbbreviation = "C", CompareUnitAmount = 4 },
                new UnitConversion { UnitConversionId = 84, UnitAbbreviation = "QT", UnitAmount = 1, CompareUnitAbbreviation = "LB", CompareUnitAmount = 2.086351113005 },
                new UnitConversion { UnitConversionId = 85, UnitAbbreviation = "QT", UnitAmount = 1, CompareUnitAbbreviation = "TBSP", CompareUnitAmount = 64 },
                new UnitConversion { UnitConversionId = 86, UnitAbbreviation = "QT", UnitAmount = 1, CompareUnitAbbreviation = "TSP", CompareUnitAmount = 192 },
                new UnitConversion { UnitConversionId = 87, UnitAbbreviation = "QT", UnitAmount = 1, CompareUnitAbbreviation = "FL OZ", CompareUnitAmount = 32 },
                new UnitConversion { UnitConversionId = 88, UnitAbbreviation = "QT", UnitAmount = 1, CompareUnitAbbreviation = "PT", CompareUnitAmount = 2 },
                new UnitConversion { UnitConversionId = 89, UnitAbbreviation = "QT", UnitAmount = 1, CompareUnitAbbreviation = "QT", CompareUnitAmount = 1 },
                new UnitConversion { UnitConversionId = 90, UnitAbbreviation = "QT", UnitAmount = 1, CompareUnitAbbreviation = "GAL", CompareUnitAmount = 0.25 },
                new UnitConversion { UnitConversionId = 91, UnitAbbreviation = "GAL", UnitAmount = 1, CompareUnitAbbreviation = "G", CompareUnitAmount = 3785.4117840007 },
                new UnitConversion { UnitConversionId = 92, UnitAbbreviation = "GAL", UnitAmount = 1, CompareUnitAbbreviation = "OZ", CompareUnitAmount = 133.52647123233 },
                new UnitConversion { UnitConversionId = 93, UnitAbbreviation = "GAL", UnitAmount = 1, CompareUnitAbbreviation = "C", CompareUnitAmount = 16 },
                new UnitConversion { UnitConversionId = 94, UnitAbbreviation = "GAL", UnitAmount = 1, CompareUnitAbbreviation = "LB", CompareUnitAmount = 8.345404452021 },
                new UnitConversion { UnitConversionId = 95, UnitAbbreviation = "GAL", UnitAmount = 1, CompareUnitAbbreviation = "TBSP", CompareUnitAmount = 256 },
                new UnitConversion { UnitConversionId = 96, UnitAbbreviation = "GAL", UnitAmount = 1, CompareUnitAbbreviation = "TSP", CompareUnitAmount = 768 },
                new UnitConversion { UnitConversionId = 97, UnitAbbreviation = "GAL", UnitAmount = 1, CompareUnitAbbreviation = "FL OZ", CompareUnitAmount = 128 },
                new UnitConversion { UnitConversionId = 98, UnitAbbreviation = "GAL", UnitAmount = 1, CompareUnitAbbreviation = "PT", CompareUnitAmount = 8 },
                new UnitConversion { UnitConversionId = 99, UnitAbbreviation = "GAL", UnitAmount = 1, CompareUnitAbbreviation = "QT", CompareUnitAmount = 4 },
                new UnitConversion { UnitConversionId = 100, UnitAbbreviation = "GAL", UnitAmount = 1, CompareUnitAbbreviation = "GAL", CompareUnitAmount = 1 }
            );

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}