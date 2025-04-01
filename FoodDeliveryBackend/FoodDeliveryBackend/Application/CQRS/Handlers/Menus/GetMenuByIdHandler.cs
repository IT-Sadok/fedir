using AutoMapper;
using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryBackend.Application.CQRS.Handlers.Menus
{

    public class GetMenuByIdHandler :
        IRequestHandler<GetMenuByIdQuery, MenuDto?>
    {
        private readonly FoodDeliveryDbContext _context;
        private readonly IMapper _mapper;
        public GetMenuByIdHandler(FoodDeliveryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MenuDto?> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.FindAsync(request.Id);
            return _mapper.Map<MenuDto>(menu);
        }
    }
}
