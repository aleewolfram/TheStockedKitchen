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

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}