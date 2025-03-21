﻿using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Queries
{
    public record GetMenuByIdQuery(int Id) : IRequest<MenuDto?>;
}
