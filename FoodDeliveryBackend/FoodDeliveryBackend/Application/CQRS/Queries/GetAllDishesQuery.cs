using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Queries
{
    public record GetAllDishesQuery() : IRequest<IEnumerable<DishDto>>;
}
