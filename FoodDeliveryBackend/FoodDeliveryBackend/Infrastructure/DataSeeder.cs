using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryBackend.Persistence
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await dbContext.Database.MigrateAsync();

            if (await dbContext.Users.AnyAsync() || await dbContext.Roles.AnyAsync() || await dbContext.Restaurants.AnyAsync())
            {
                return;
            }

            string[] roles = { "Customer", "RestaurantOwner", "Courier", "Admin" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com", FullName = "Admin User" },
                new ApplicationUser { UserName = "customer@example.com", Email = "customer@example.com", FullName = "Regular Customer" },
                new ApplicationUser { UserName = "owner@example.com", Email = "owner@example.com", FullName = "Restaurant Owner" },
                new ApplicationUser { UserName = "courier@example.com", Email = "courier@example.com", FullName = "Delivery Courier" }
            };

            foreach (var user in users)
            {
                var existingUser = await userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    await userManager.CreateAsync(user, "Test@123");
                    await userManager.AddToRoleAsync(user, roles[users.IndexOf(user)]);
                }
            }

            var ownerUser = await userManager.FindByEmailAsync("owner@example.com");

            // ✅ Seed Restaurants
            var restaurants = new List<Restaurant>
            {
                new Restaurant { Name = "Sushi Express", Address = "123 Ocean Ave", OwnerId = ownerUser.Id },
                new Restaurant { Name = "Pizza Heaven", Address = "456 Main St", OwnerId = ownerUser.Id },
                new Restaurant { Name = "Kebab House", Address = "789 Market St", OwnerId = ownerUser.Id },
                new Restaurant { Name = "Ukrainian Delights", Address = "101 Kyiv Ave", OwnerId = ownerUser.Id }
            };
            await dbContext.Restaurants.AddRangeAsync(restaurants);
            await dbContext.SaveChangesAsync();

            // ✅ Seed Menus
            var menus = restaurants.Select(r => new Menu { Name = $"{r.Name} Menu", RestaurantId = r.Id }).ToList();
            await dbContext.Menus.AddRangeAsync(menus);
            await dbContext.SaveChangesAsync();

            // ✅ Seed Dishes
            var dishes = new List<Dish>
            {
                // Sushi Restaurant Dishes
                new Dish { Name = "California Roll", Price = 12.99m, MenuId = menus[0].Id },
                new Dish { Name = "Spicy Tuna Roll", Price = 14.99m, MenuId = menus[0].Id },
                new Dish { Name = "Salmon Nigiri", Price = 10.99m, MenuId = menus[0].Id },
                new Dish { Name = "Miso Soup", Price = 5.99m, MenuId = menus[0].Id },
                new Dish { Name = "Sashimi Platter", Price = 22.99m, MenuId = menus[0].Id },

                // Pizza Restaurant Dishes
                new Dish { Name = "Pepperoni Pizza", Price = 9.99m, MenuId = menus[1].Id },
                new Dish { Name = "Margherita Pizza", Price = 8.99m, MenuId = menus[1].Id },
                new Dish { Name = "BBQ Chicken Pizza", Price = 11.99m, MenuId = menus[1].Id },
                new Dish { Name = "Vegetarian Pizza", Price = 10.99m, MenuId = menus[1].Id },
                new Dish { Name = "Cheese Pizza", Price = 7.99m, MenuId = menus[1].Id },

                // Kebab Restaurant Dishes
                new Dish { Name = "Chicken Kebab", Price = 9.99m, MenuId = menus[2].Id },
                new Dish { Name = "Lamb Kebab", Price = 12.99m, MenuId = menus[2].Id },
                new Dish { Name = "Falafel Wrap", Price = 7.99m, MenuId = menus[2].Id },
                new Dish { Name = "Shawarma Plate", Price = 14.99m, MenuId = menus[2].Id },
                new Dish { Name = "Hummus & Pita", Price = 5.99m, MenuId = menus[2].Id },

                // Ukrainian Food Dishes
                new Dish { Name = "Borscht", Price = 6.99m, MenuId = menus[3].Id },
                new Dish { Name = "Varenyky (Dumplings)", Price = 9.99m, MenuId = menus[3].Id },
                new Dish { Name = "Holubtsi (Stuffed Cabbage)", Price = 10.99m, MenuId = menus[3].Id },
                new Dish { Name = "Deruny (Potato Pancakes)", Price = 7.99m, MenuId = menus[3].Id },
                new Dish { Name = "Kutia (Sweet Grain Pudding)", Price = 5.99m, MenuId = menus[3].Id },

                // Common Options
                new Dish { Name = "Coca-Cola", Price = 1.99m, MenuId = menus[0].Id },
                new Dish { Name = "Coca-Cola", Price = 1.99m, MenuId = menus[1].Id },
                new Dish { Name = "Coca-Cola", Price = 1.99m, MenuId = menus[2].Id },
                new Dish { Name = "Coca-Cola", Price = 1.99m, MenuId = menus[3].Id },
            };
            await dbContext.Dishes.AddRangeAsync(dishes);
            await dbContext.SaveChangesAsync();
        }
    }
}
