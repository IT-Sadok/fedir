using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Commands
{
    public record UpdateDishCommand(int Id, DishDto DishDto) : IRequest;
}
