using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryBackend.Application.CQRS.Handlers.Restoraunt
{
    public class GetAllRestaurantsHandler :
        IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
    {

        private readonly FoodDeliveryDbContext _context;
        public GetAllRestaurantsHandler(FoodDeliveryDbContext context) { _context = context; }

        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Restaurants.Select(r => new RestaurantDto { Id = r.Id, Name = r.Name }).ToListAsync();
        }
    }
}
