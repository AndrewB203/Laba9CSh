using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Models;

namespace NorthwindAPI.Data
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Product>().HasKey(p => p.ProductID);
            modelBuilder.Entity<Product>().Property(p => p.ProductID).HasColumnName("product_id");
            modelBuilder.Entity<Product>().Property(p => p.ProductName).HasColumnName("product_name");
            modelBuilder.Entity<Product>().Property(p => p.SupplierID).HasColumnName("supplier_id");
            modelBuilder.Entity<Product>().Property(p => p.CategoryID).HasColumnName("category_id");
            modelBuilder.Entity<Product>().Property(p => p.QuantityPerUnit).HasColumnName("quantity_per_unit");
            modelBuilder.Entity<Product>().Property(p => p.UnitPrice).HasColumnName("unit_price");
            modelBuilder.Entity<Product>().Property(p => p.UnitsInStock).HasColumnName("units_in_stock");
            modelBuilder.Entity<Product>().Property(p => p.UnitsOnOrder).HasColumnName("units_on_order");
            modelBuilder.Entity<Product>().Property(p => p.ReorderLevel).HasColumnName("reorder_level");
            modelBuilder.Entity<Product>().Property(p => p.Discontinued).HasColumnName("discontinued").HasColumnType("integer");
        }
    }
}