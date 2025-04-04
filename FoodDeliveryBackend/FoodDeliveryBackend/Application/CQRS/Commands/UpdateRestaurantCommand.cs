﻿using FoodDeliveryBackend.Domain.DTO;
using MediatR;

namespace FoodDeliveryBackend.Application.CQRS.Commands
{
    public record UpdateRestaurantCommand(string Id, RestaurantDto RestaurantDto) : IRequest;
}
