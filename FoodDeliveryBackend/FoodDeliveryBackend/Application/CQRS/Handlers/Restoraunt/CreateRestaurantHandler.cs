using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryBackend.Application.CQRS.Handlers.Restoraunt
{
    public class CreateRestaurantHandler :
        IRequestHandler<CreateRestaurantCommand>
    {
        private readonly FoodDeliveryDbContext _context;
        public CreateRestaurantHandler(FoodDeliveryDbContext context) { _context = context; }

        public async Task Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = new Restaurant { Name = request.RestaurantDto.Name };
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}
