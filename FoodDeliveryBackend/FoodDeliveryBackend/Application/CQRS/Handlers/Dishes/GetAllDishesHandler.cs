using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;

namespace FoodDeliveryBackend.Application.CQRS.Handlers.Dishes
{
    public class GetAllDishesHandler :
        IRequestHandler<GetAllDishesQuery, IEnumerable<DishDto>>
    {
        private readonly FoodDeliveryDbContext _context;
        public GetAllDishesHandler(FoodDeliveryDbContext context) { _context = context; }

        public async Task<IEnumerable<DishDto>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Dishes.Select(d => new DishDto { Id = d.Id, Name = d.Name, MenuId = d.MenuId }).ToListAsync();
        }
    }
}
