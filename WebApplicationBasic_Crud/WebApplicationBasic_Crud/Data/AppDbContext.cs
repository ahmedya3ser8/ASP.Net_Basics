using Microsoft.EntityFrameworkCore;
using WebApplicationBasic_Crud.Entities;

namespace WebApplicationBasic_Crud.Data
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
