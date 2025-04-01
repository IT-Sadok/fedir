using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryBackend.Application.CQRS.Handlers.Restoraunt
{
    public class DeleteRestaurantHandler :
        IRequestHandler<DeleteRestaurantCommand>
    {

        private readonly FoodDeliveryDbContext _context;
        public DeleteRestaurantHandler(FoodDeliveryDbContext context) { _context = context; }

        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _context.Restaurants.FindAsync(request.Id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
            }
        }
    }
}
