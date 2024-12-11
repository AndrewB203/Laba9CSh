using Microsoft.EntityFrameworkCore;

namespace StockAnalyzer.Models
{
    public class StockDbContext : DbContext
    {
        public DbSet<StockPrice> StockPrices { get; set; }
        public DbSet<TodaysCondition> TodaysConditions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=lab_user;Password=12345;Database=lab_user");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockPrice>().HasKey(s => s.Id);
            modelBuilder.Entity<TodaysCondition>().HasKey(t => t.Id);
        }
    }
}