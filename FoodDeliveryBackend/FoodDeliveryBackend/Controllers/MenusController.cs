using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using FoodDeliveryBackend.Application.Services.CQRS;
using FoodDeliveryBackend.Domain.DTO;

namespace FoodDeliveryBackend.Controllers
{
    [Route("api/menus")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MenuController(IMediator mediator) { _mediator = mediator; }

        [HttpGet]
        public async Task<IActionResult> GetAllMenus()
        {
            return Ok(await _mediator.Send(new GetAllMenusQuery()));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetMenuById(int id)
        {
            return Ok(await _mediator.Send(new GetMenuByIdQuery(id)));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,RestaurantOwner")]
        public async Task<IActionResult> CreateMenu([FromBody] MenuDto menuDto)
        {
            await _mediator.Send(new CreateMenuCommand(menuDto));
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,RestaurantOwner")]
        public async Task<IActionResult> UpdateMenu(int id, [FromBody] MenuDto menuDto)
        {
            await _mediator.Send(new UpdateMenuCommand(id, menuDto));
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,RestaurantOwner")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            await _mediator.Send(new DeleteMenuCommand(id));
            return Ok();
        }
    }
}
