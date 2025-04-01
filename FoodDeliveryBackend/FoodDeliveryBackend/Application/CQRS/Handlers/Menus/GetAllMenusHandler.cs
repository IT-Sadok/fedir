using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryBackend.Application.CQRS.Handlers.Menus
{

    public class GetAllMenusHandler :
        IRequestHandler<GetAllMenusQuery, IEnumerable<MenuDto>>
    {
        private readonly FoodDeliveryDbContext _context;
        public GetAllMenusHandler(FoodDeliveryDbContext context) { _context = context; }

        public async Task<IEnumerable<MenuDto>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            return await _context.Menus.Select(m => new MenuDto { Id = m.Id, RestaurantId = m.RestaurantId }).ToListAsync();
        }
    }
}
