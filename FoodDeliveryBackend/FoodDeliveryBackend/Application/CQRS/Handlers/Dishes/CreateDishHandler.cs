using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;

namespace FoodDeliveryBackend.Application.CQRS.Handlers
{
    public class CreateDishHandler :
        IRequestHandler<CreateDishCommand>
    {
        private readonly FoodDeliveryDbContext _context;
        public CreateDishHandler(FoodDeliveryDbContext context) { _context = context; }


        public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            var dish = new Dish { Name = request.DishDto.Name, MenuId = request.DishDto.MenuId };
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
        }
    }
}
