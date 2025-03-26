using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Queries
{
    public record GetMenuByIdQuery(string Id) : IRequest<MenuDto?>;
}
