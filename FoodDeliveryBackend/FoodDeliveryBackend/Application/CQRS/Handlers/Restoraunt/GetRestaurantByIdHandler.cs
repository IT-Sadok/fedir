using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Persistence;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Handlers.Restoraunt
{
    public class GetRestaurantByIdHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
    {
        private readonly FoodDeliveryDbContext _context;
        public GetRestaurantByIdHandler(FoodDeliveryDbContext context) { _context = context; }

        public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await _context.Restaurants.FindAsync(request.Id);
            return restaurant == null ? null : new RestaurantDto { Id = restaurant.Id, Name = restaurant.Name };
        }
    }
}
