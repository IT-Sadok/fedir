using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Commands
{
    public record UpdateRestaurantCommand(int Id, RestaurantDto RestaurantDto) : IRequest;
}
