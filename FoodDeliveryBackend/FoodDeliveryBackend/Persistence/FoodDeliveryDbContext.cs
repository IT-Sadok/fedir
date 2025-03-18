using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryBackend.Identity;
using FoodDeliveryBackend.Domain.Entities;
using System.Reflection.Emit;

namespace FoodDeliveryBackend.Persistence
{
    public class FoodDeliveryDbContext : IdentityDbContext<ApplicationUser>
    {
        public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options) : base(options) { }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER" },
                new IdentityRole { Name = "RestaurantOwner", NormalizedName = "RESTAURANTOWNER" },
                new IdentityRole { Name = "Courier", NormalizedName = "COURIER" },
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
            );

            builder.Entity<Restaurant>()
                .HasOne(r => r.Menu)
                .WithOne(m => m.Restaurant)
                .HasForeignKey<Menu>(m => m.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Menu>()
                .HasMany(m => m.Dishes)
                .WithOne(d => d.Menu)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}