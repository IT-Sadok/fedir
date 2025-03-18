using FoodDeliveryBackend.Application.Services.Interfaces;
using FoodDeliveryBackend.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryBackend.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService) { _restaurantService = restaurantService; }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllRestaurants()
        {
            return Ok(await _restaurantService.GetAllRestaurantsAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            return Ok(await _restaurantService.GetRestaurantByIdAsync(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,RestaurantOwner")]
        public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantDto restaurantDto)
        {
            await _restaurantService.CreateRestaurantAsync(restaurantDto);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,RestaurantOwner")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] RestaurantDto restaurantDto)
        {
            await _restaurantService.UpdateRestaurantAsync(id, restaurantDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,RestaurantOwner")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            await _restaurantService.DeleteRestaurantAsync(id);
            return Ok();
        }
    }
}
