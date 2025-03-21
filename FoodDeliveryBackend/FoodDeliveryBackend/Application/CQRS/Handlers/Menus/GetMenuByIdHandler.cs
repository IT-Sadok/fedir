using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryBackend.Application.CQRS.Handlers.Menus
{

    public class GetMenuByIdHandler :
        IRequestHandler<GetMenuByIdQuery, MenuDto?>
    {
        private readonly FoodDeliveryDbContext _context;
        public GetMenuByIdHandler(FoodDeliveryDbContext context) { _context = context; }

        public async Task<MenuDto?> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.FindAsync(request.Id);
            return menu == null ? null : new MenuDto { Id = menu.Id, RestaurantId = menu.RestaurantId };
        }
    }
}
