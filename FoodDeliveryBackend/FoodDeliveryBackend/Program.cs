using FoodDeliveryBackend.Application.Services;
using FoodDeliveryBackend.Application.Services.Interfaces;
using FoodDeliveryBackend.Identity;
using FoodDeliveryBackend.MinimalAPI;
using FoodDeliveryBackend.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class Program
{
    protected Program()
    {
    }

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure database
        builder.Services.AddDbContext<FoodDeliveryDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Configure Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<FoodDeliveryDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        // Configure JWT Authentication
        var key = Encoding.UTF8.GetBytes("Jwt:Secret");
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        // Enable controllers
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(typeof(MappingProfile));

        var app = builder.Build();

        await DatabaseSeeder.SeedDatabaseAsync(app.Services);

        // Enable authentication & authorization
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.RegisterRestaurantEndpoints();
        app.RegisterMenusEndpoints();
        app.RegisterDishEndpoints();

        app.Run();
    }
}

