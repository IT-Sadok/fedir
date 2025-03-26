using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryBackend.MinimalAPI
{
    public static class RestaurantEndpoints
    {
        public static void RegisterRestaurantEndpoints(this WebApplication app)
        {
            app.MapGet("/api/restaurants", async (IMediator _mediator) =>
            {
                var result = await _mediator.Send(new GetAllRestaurantsQuery());
                return Results.Ok(result);
            });

            app.MapGet("/api/restaurants/{id}", async (string id,IMediator _mediator) =>
            {
                var result = await _mediator.Send(new GetRestaurantByIdQuery(id));
                return Results.Ok(result);
            });

            app.MapPost("/api/restaurants", (RestaurantDto restaurantDto, IMediator _mediator) =>
            {
                var result = _mediator.Send(new CreateRestaurantCommand(restaurantDto));
                return Results.Ok(result);
            }).RequireAuthorization("Admin", "RestaurantOwner");

            app.MapPut("/api/restaurants/{id}", (string id,  RestaurantDto restaurantDto, IMediator _mediator) =>
            {
                var result = _mediator.Send(new UpdateRestaurantCommand(id, restaurantDto));
                return Results.Ok(result);
            }).RequireAuthorization("Admin", "RestaurantOwner");

            app.MapDelete("/api/restaurants/{id}", (string id,  IMediator _mediator) =>
            {
                var result = _mediator.Send(new DeleteRestaurantCommand(id));
                return Results.Ok(result);
            }).RequireAuthorization("Admin", "RestaurantOwner");
        }
    }
}
