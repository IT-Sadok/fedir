using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryBackend.Application.CQRS.Handlers.Restoraunt
{
    public class UpdateRestaurantHandler :
        IRequestHandler<UpdateRestaurantCommand>
    {
        private readonly FoodDeliveryDbContext _context;
        public UpdateRestaurantHandler(FoodDeliveryDbContext context) { _context = context; }
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _context.Restaurants.FindAsync(request.Id);
            if (restaurant != null)
            {
                restaurant.Name = request.RestaurantDto.Name;
                await _context.SaveChangesAsync();
            }
        }
    }
}
