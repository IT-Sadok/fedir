using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;

namespace FoodDeliveryBackend.CQRS.Restaurants
{
    public record GetAllDishesQuery() : IRequest<IEnumerable<DishDto>>;
    public record GetDishByIdQuery(int Id) : IRequest<DishDto?>;
    public record CreateDishCommand(DishDto DishDto) : IRequest;
    public record UpdateDishCommand(int Id, DishDto DishDto) : IRequest;
    public record DeleteDishCommand(int Id) : IRequest;
    public class DishHandlers :
        IRequestHandler<GetAllDishesQuery, IEnumerable<DishDto>>,
        IRequestHandler<GetDishByIdQuery, DishDto?>,
        IRequestHandler<CreateDishCommand>,
        IRequestHandler<UpdateDishCommand>,
        IRequestHandler<DeleteDishCommand>
    {
        private readonly FoodDeliveryDbContext _context;
        public DishHandlers(FoodDeliveryDbContext context) { _context = context; }

        public async Task<IEnumerable<DishDto>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Dishes.Select(d => new DishDto { Id = d.Id, Name = d.Name, MenuId = d.MenuId }).ToListAsync();
        }

        public async Task<DishDto?> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            var dish = await _context.Dishes.FindAsync(request.Id);
            return dish == null ? null : new DishDto { Id = dish.Id, Name = dish.Name, MenuId = dish.MenuId };
        }

        public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            var dish = new Dish { Name = request.DishDto.Name, MenuId = request.DishDto.MenuId };
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
        }

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

        public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await _context.Dishes.FindAsync(request.Id);
            if (dish != null)
            {
                _context.Dishes.Remove(dish);
                await _context.SaveChangesAsync();
            }
        }
    }
}
