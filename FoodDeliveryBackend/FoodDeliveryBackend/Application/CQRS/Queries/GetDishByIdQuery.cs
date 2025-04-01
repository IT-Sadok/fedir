using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Queries
{
    public record GetDishByIdQuery(string Id) : IRequest<DishDto?>;
}
