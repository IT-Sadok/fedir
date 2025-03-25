using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using AutoMapper;

namespace FoodDeliveryBackend.Application.CQRS.Handlers
{
    public class GetDishByIdHandler : IRequestHandler<GetDishByIdQuery, DishDto?>
    {
        private readonly FoodDeliveryDbContext _context;
        private readonly IMapper _mapper;

        public GetDishByIdHandler(FoodDeliveryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DishDto?> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            var dish = await _context.Dishes.FindAsync(request.Id);
            return _mapper.Map<DishDto>(dish);
        }
    }
}
