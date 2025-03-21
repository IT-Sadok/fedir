using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryBackend.Controllers
{
    [Route("api/dishes")]
    [ApiController]
    public class DishesController : Controller
    {
        private readonly IMediator _mediator;
        public DishesController(IMediator mediator) { _mediator = mediator; }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllDishs()
        {
            return Ok(await _mediator.Send(new GetAllDishesQuery()));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetDishById(int id)
        {
            return Ok(await _mediator.Send(new GetDishByIdQuery(id)));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,DishOwner")]
        public async Task<IActionResult> CreateDish([FromBody] DishDto DishDto)
        {
            await _mediator.Send(new CreateDishCommand(DishDto));
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,DishOwner")]
        public async Task<IActionResult> UpdateDish(int id, [FromBody] DishDto DishDto)
        {
            await _mediator.Send(new UpdateDishCommand(id, DishDto));
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,DishOwner")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            await _mediator.Send(new DeleteDishCommand(id));
            return Ok();
        }
    }
}
