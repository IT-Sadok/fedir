using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Commands
{
    public record UpdateDishCommand(string Id, DishDto DishDto) : IRequest;
}
