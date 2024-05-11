using Microsoft.EntityFrameworkCore;
using WebApplication_Middleware.Entities;

namespace WebApplication_Middleware.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Products");
        }

    }
}
