using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryBackend.Application.CQRS.Handlers.Menus
{

    public class UpdateMenuHandler :
        IRequestHandler<UpdateMenuCommand>
    {
        private readonly FoodDeliveryDbContext _context;
        public UpdateMenuHandler(FoodDeliveryDbContext context) { _context = context; }

        public async Task Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.FindAsync(request.Id);
            if (menu != null)
            {
                menu.RestaurantId = request.MenuDto.RestaurantId;
                await _context.SaveChangesAsync();
            }
        }
    }
}
