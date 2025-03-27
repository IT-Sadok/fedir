using Microsoft.EntityFrameworkCore;
using FoodDeliveryBackend.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing DB context
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<FoodDeliveryDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            // Add an in-memory database for testing
            services.AddDbContext<FoodDeliveryDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Build the service provider
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FoodDeliveryDbContext>();
                db.Database.EnsureCreated(); // Seed test data if needed
            }
        });
    }
}
