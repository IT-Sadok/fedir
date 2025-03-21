using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Commands
{
    public record UpdateMenuCommand(int Id, MenuDto MenuDto) : IRequest;
}
