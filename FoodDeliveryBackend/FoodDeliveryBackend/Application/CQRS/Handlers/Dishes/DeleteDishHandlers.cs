using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryBackend.Domain.DTO;
using FoodDeliveryBackend.Domain.Entities;
using FoodDeliveryBackend.Persistence;
using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;

namespace FoodDeliveryBackend.Application.CQRS.Handlers
{
    public class DeleteDishHandlers :
        IRequestHandler<DeleteDishCommand>
    {
        private readonly FoodDeliveryDbContext _context;
        public DeleteDishHandlers(FoodDeliveryDbContext context) { _context = context; }

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
