using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Queries
{
    public record GetRestaurantByIdQuery(string Id) : IRequest<RestaurantDto?>;
}
