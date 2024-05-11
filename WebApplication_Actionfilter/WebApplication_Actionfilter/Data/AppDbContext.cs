using Microsoft.EntityFrameworkCore;
using WebApplication_Actionfilter.Entities;

namespace WebApplication_Actionfilter.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Products");
        }

    }
}
