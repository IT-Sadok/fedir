using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryBackend.Application.Services.CQRS
{
    public record GetAllRestaurantsQuery() : IRequest<IEnumerable<RestaurantDto>>;
    public record GetRestaurantByIdQuery(int Id) : IRequest<RestaurantDto?>;
    public record CreateRestaurantCommand(RestaurantDto RestaurantDto) : IRequest;
    public record UpdateRestaurantCommand(int Id, RestaurantDto RestaurantDto) : IRequest;
    public record DeleteRestaurantCommand(int Id) : IRequest;

    public class RestaurantHandlers :
        IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>,
        IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>,
        IRequestHandler<CreateRestaurantCommand>,
        IRequestHandler<UpdateRestaurantCommand>,
        IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly FoodDeliveryDbContext _context;
        public RestaurantHandlers(FoodDeliveryDbContext context) { _context = context; }

        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Restaurants.Select(r => new RestaurantDto { Id = r.Id, Name = r.Name }).ToListAsync();
        }

        public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await _context.Restaurants.FindAsync(request.Id);
            return restaurant == null ? null : new RestaurantDto { Id = restaurant.Id, Name = restaurant.Name };
        }

        public async Task Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = new Restaurant { Name = request.RestaurantDto.Name };
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _context.Restaurants.FindAsync(request.Id);
            if (restaurant != null)
            {
                restaurant.Name = request.RestaurantDto.Name;
                await _context.SaveChangesAsync();
            }
        }

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
