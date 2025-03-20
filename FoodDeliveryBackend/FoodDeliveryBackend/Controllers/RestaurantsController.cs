using FoodDeliveryBackend.Application.Services.CQRS;
using FoodDeliveryBackend.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryBackend.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IMediator _mediator;
        public RestaurantController(IMediator mediator) { _mediator = mediator; }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllRestaurants()
        {
            return Ok(await _mediator.Send(new GetAllRestaurantsQuery()));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            return Ok(await _mediator.Send(new GetRestaurantByIdQuery(id)));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,RestaurantOwner")]
        public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantDto restaurantDto)
        {
            await _mediator.Send(new CreateRestaurantCommand(restaurantDto));
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,RestaurantOwner")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] RestaurantDto restaurantDto)
        {
            await _mediator.Send(new UpdateRestaurantCommand(id, restaurantDto));
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,RestaurantOwner")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            await _mediator.Send(new DeleteRestaurantCommand(id));
            return Ok();
        }
    }
}
