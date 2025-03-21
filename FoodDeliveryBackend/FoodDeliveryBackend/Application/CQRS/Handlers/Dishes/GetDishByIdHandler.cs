using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;

namespace FoodDeliveryBackend.Application.CQRS.Handlers
{
    public class GetDishByIdHandler :
        IRequestHandler<GetDishByIdQuery, DishDto?>
    {
        private readonly FoodDeliveryDbContext _context;
        public GetDishByIdHandler(FoodDeliveryDbContext context) { _context = context; }

        public async Task<DishDto?> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            var dish = await _context.Dishes.FindAsync(request.Id);
            return dish == null ? null : new DishDto { Id = dish.Id, Name = dish.Name, MenuId = dish.MenuId };
        }
    }
}
