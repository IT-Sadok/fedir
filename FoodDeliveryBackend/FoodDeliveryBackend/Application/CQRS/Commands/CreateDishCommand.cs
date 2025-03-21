using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Commands
{
    public record CreateDishCommand(DishDto DishDto) : IRequest;
}
