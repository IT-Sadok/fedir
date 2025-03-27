using System.Net;
using FluentAssertions;
using FoodDeliveryBackend.Domain.DTO;

public class RestaurantsApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public RestaurantsApiTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllRestaurants_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("api/restaurants/all");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task CreateRestaurant_ReturnsSuccess()
    {
        // Arrange
        var newRestaurant = new RestaurantDto { Name = "Test Restaurant" };

        // Act
        var response = await _client.PostAsJsonAsync("api/restaurants", newRestaurant);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
