using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Commands
{
    public record DeleteMenuCommand(string Id) : IRequest;
}
