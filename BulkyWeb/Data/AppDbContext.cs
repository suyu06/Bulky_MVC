using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        // create table Categories in the db Bulky
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Category>().HasData(
                new Category {Id = 1, CategoryName="Action", DisplayOrder=1},
                new Category {Id = 2, CategoryName = "SciFi", DisplayOrder = 2 },
                new Category {Id = 3, CategoryName = "History", DisplayOrder =3 }
                );
            
        }
    }
}
