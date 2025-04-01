using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;

namespace FoodDeliveryBackend.Application.CQRS.Handlers
{
    public class UpdateDishHandler :
        IRequestHandler<UpdateDishCommand>
    {
        private readonly FoodDeliveryDbContext _context;
        public UpdateDishHandler(FoodDeliveryDbContext context) { _context = context; }

        public async Task Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await _context.Dishes.FindAsync(request.Id);
            if (dish != null)
            {
                dish.Name = request.DishDto.Name;
                dish.MenuId = request.DishDto.MenuId;
                await _context.SaveChangesAsync();
            }
        }
    }
}
