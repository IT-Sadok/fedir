using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Queries
{
    public record GetAllMenusQuery() : IRequest<IEnumerable<MenuDto>>;
}
