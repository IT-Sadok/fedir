using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryBackend.Application.Services.CQRS
{

    public record GetAllMenusQuery() : IRequest<IEnumerable<MenuDto>>;
    public record GetMenuByIdQuery(int Id) : IRequest<MenuDto?>;
    public record CreateMenuCommand(MenuDto MenuDto) : IRequest;
    public record UpdateMenuCommand(int Id, MenuDto MenuDto) : IRequest;
    public record DeleteMenuCommand(int Id) : IRequest;

    public class MenuHandlers :
        IRequestHandler<GetAllMenusQuery, IEnumerable<MenuDto>>,
        IRequestHandler<GetMenuByIdQuery, MenuDto?>,
        IRequestHandler<CreateMenuCommand>,
        IRequestHandler<UpdateMenuCommand>,
        IRequestHandler<DeleteMenuCommand>
    {
        private readonly FoodDeliveryDbContext _context;
        public MenuHandlers(FoodDeliveryDbContext context) { _context = context; }

        public async Task<IEnumerable<MenuDto>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            return await _context.Menus.Select(m => new MenuDto { Id = m.Id, RestaurantId = m.RestaurantId }).ToListAsync();
        }

        public async Task<MenuDto?> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.FindAsync(request.Id);
            return menu == null ? null : new MenuDto { Id = menu.Id, RestaurantId = menu.RestaurantId };
        }

        public async Task Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = new Menu { RestaurantId = request.MenuDto.RestaurantId };
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
        }

        public async Task Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.FindAsync(request.Id);
            if (menu != null)
            {
                menu.RestaurantId = request.MenuDto.RestaurantId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.FindAsync(request.Id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
            }
        }
    }
}
