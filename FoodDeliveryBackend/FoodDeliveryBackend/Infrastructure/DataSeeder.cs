using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryBackend.Persistence
{
    public static class DatabaseSeeder
    {
        private const string fullNameAdmin = "Admin User";
        private const string fullNameCustomer = "Regular Customer";
        private const string fullNameRestaurantOwner = "Restaurant Owner";
        private const string fullNameCourier = "Delivery Courier";
        public static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<FoodDeliveryDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await dbContext.Database.MigrateAsync();

            if (await dbContext.Users.AnyAsync() || await dbContext.Restaurants.AnyAsync())
            {
                return;
            }

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com", FullName = fullNameAdmin },
                new ApplicationUser { UserName = "customer@example.com", Email = "customer@example.com", FullName = fullNameCustomer },
                new ApplicationUser { UserName = "owner1@example.com", Email = "owner1@example.com", FullName = fullNameRestaurantOwner },
                new ApplicationUser { UserName = "owner2@example.com", Email = "owner2@example.com", FullName = fullNameRestaurantOwner },
                new ApplicationUser { UserName = "owner3@example.com", Email = "owner3@example.com", FullName = fullNameRestaurantOwner },
                new ApplicationUser { UserName = "owner4@example.com", Email = "owner4@example.com", FullName = fullNameRestaurantOwner },
                new ApplicationUser { UserName = "courier@example.com", Email = "courier@example.com", FullName = fullNameCourier }
            };

            foreach (var user in users)
            {
                var existingUser = await userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    await userManager.CreateAsync(user, "Test@123");

                    switch (user.FullName)
                    {
                        case fullNameAdmin:
                            await userManager.AddToRoleAsync(user, "Admin");
                            break;
                        case fullNameCustomer:
                            await userManager.AddToRoleAsync(user, "Customer");
                            break;
                        case fullNameRestaurantOwner:
                            await userManager.AddToRoleAsync(user, "RestaurantOwner");
                            break;
                        case fullNameCourier:
                            await userManager.AddToRoleAsync(user, "Courier");
                            break;
                    }
                }
            }

            await dbContext.SaveChangesAsync();

            var ownerUser1 = await userManager.FindByEmailAsync("owner1@example.com");
            var ownerUser2 = await userManager.FindByEmailAsync("owner2@example.com");
            var ownerUser3 = await userManager.FindByEmailAsync("owner3@example.com");
            var ownerUser4 = await userManager.FindByEmailAsync("owner4@example.com");

            // Seed Restaurants
            var restaurants = new List<Restaurant>
            {
                new Restaurant { Id = Guid.NewGuid().ToString(), Name = "Sushi Express", Address = "123 Ocean Ave", OwnerId = ownerUser1.Id },
                new Restaurant { Id = Guid.NewGuid().ToString(),  Name = "Pizza Heaven", Address = "456 Main St", OwnerId = ownerUser2.Id },
                new Restaurant { Id = Guid.NewGuid().ToString(),  Name = "Kebab House", Address = "789 Market St", OwnerId = ownerUser3.Id },
                new Restaurant { Id = Guid.NewGuid().ToString(),  Name = "Ukrainian Delights", Address = "101 Kyiv Ave", OwnerId = ownerUser4.Id }
            };
            await dbContext.Restaurants.AddRangeAsync(restaurants);
            await dbContext.SaveChangesAsync();

            // Seed Menus
            var menus = restaurants.Select(r => new Menu { Id = Guid.NewGuid().ToString(), Name = $"{r.Name} Menu", RestaurantId = r.Id }).ToList();
            await dbContext.Menus.AddRangeAsync(menus);
            await dbContext.SaveChangesAsync();

            // Seed Dishes
            var dishes = new List<Dish>
            {
                // Sushi Restaurant Dishes
                new Dish { Id = Guid.NewGuid().ToString(), Name = "California Roll", Price = 12.99m, MenuId = menus[0].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Spicy Tuna Roll", Price = 14.99m, MenuId = menus[0].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Salmon Nigiri", Price = 10.99m, MenuId = menus[0].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Miso Soup", Price = 5.99m, MenuId = menus[0].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Sashimi Platter", Price = 22.99m, MenuId = menus[0].Id },

                // Pizza Restaurant Dishes
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Pepperoni Pizza", Price = 9.99m, MenuId = menus[1].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Margherita Pizza", Price = 8.99m, MenuId = menus[1].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "BBQ Chicken Pizza", Price = 11.99m, MenuId = menus[1].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Vegetarian Pizza", Price = 10.99m, MenuId = menus[1].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Cheese Pizza", Price = 7.99m, MenuId = menus[1].Id },

                // Kebab Restaurant Dishes
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Chicken Kebab", Price = 9.99m, MenuId = menus[2].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Lamb Kebab", Price = 12.99m, MenuId = menus[2].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Falafel Wrap", Price = 7.99m, MenuId = menus[2].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Shawarma Plate", Price = 14.99m, MenuId = menus[2].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Hummus & Pita", Price = 5.99m, MenuId = menus[2].Id },

                // Ukrainian Food Dishes
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Borscht", Price = 6.99m, MenuId = menus[3].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Varenyky (Dumplings)", Price = 9.99m, MenuId = menus[3].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Holubtsi (Stuffed Cabbage)", Price = 10.99m, MenuId = menus[3].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Deruny (Potato Pancakes)", Price = 7.99m, MenuId = menus[3].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Kutia (Sweet Grain Pudding)", Price = 5.99m, MenuId = menus[3].Id },

                // Common Options
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Coca-Cola", Price = 1.99m, MenuId = menus[0].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Coca-Cola", Price = 1.99m, MenuId = menus[1].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Coca-Cola", Price = 1.99m, MenuId = menus[2].Id },
                new Dish { Id = Guid.NewGuid().ToString(), Name = "Coca-Cola", Price = 1.99m, MenuId = menus[3].Id },
            };
            await dbContext.Dishes.AddRangeAsync(dishes);
            await dbContext.SaveChangesAsync();
        }
    }
}
