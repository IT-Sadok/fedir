using AutoMapper;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Persistence;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Handlers.Restoraunt
{
    public class GetRestaurantByIdHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
    {
        private readonly FoodDeliveryDbContext _context;
        private readonly IMapper _mapper;
        public GetRestaurantByIdHandler(FoodDeliveryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await _context.Restaurants.FindAsync(request.Id);
            return _mapper.Map<RestaurantDto>(restaurant);
        }
    }
}
