using Microsoft.EntityFrameworkCore;
using TheStockedKitchen.Data.Model;

namespace SCGPlanningTool.Db
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

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}