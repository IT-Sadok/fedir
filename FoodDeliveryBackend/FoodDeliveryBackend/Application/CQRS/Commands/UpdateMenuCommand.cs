using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Commands
{
    public record UpdateMenuCommand(string Id, MenuDto MenuDto) : IRequest;
}
