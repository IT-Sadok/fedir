using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Commands
{
    public record CreateMenuCommand(MenuDto MenuDto) : IRequest;
}
