using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryBackend.Application.CQRS.Handlers.Menus
{

    public class CreateMenuHandler :
        IRequestHandler<CreateMenuCommand>
    {
        private readonly FoodDeliveryDbContext _context;
        public CreateMenuHandler(FoodDeliveryDbContext context) { _context = context; }


        public async Task Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = new Menu { RestaurantId = request.MenuDto.RestaurantId };
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
        }

    }
}
