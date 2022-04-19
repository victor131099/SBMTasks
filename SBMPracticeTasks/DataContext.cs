
using Microsoft.EntityFrameworkCore;

namespace SBMPracticeTasks
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "Production");

            });
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory", "Production");
            });
            modelBuilder.Entity<ProductSubCategory>(entity =>
            {
                entity.ToTable("ProductSubCategory", "Production");
            });
        }
    }
}