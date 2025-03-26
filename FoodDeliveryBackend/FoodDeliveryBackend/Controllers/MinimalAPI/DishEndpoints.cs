using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryBackend.MinimalAPI
{
    public static class DishEndpoints
    {
        public static void RegisterDishEndpoints(this WebApplication app)
        {
            app.MapGet("/api/dishes", async (IMediator _mediator) =>
            {
                var result = await _mediator.Send(new GetAllDishesQuery());
                return Results.Ok(result);
            });

            app.MapGet("/api/dishes/{id}", async (string id,IMediator _mediator) =>
            {
                var result = await _mediator.Send(new GetDishByIdQuery(id));
                return Results.Ok(result);
            });

            app.MapPost("/api/dishes", (DishDto dishDto, IMediator _mediator) =>
            {
                var result = _mediator.Send(new CreateDishCommand(dishDto));
                return Results.Ok(result);
            }).RequireAuthorization("Admin", "RestaurantOwner");

            app.MapPut("/api/dishes/{id}", (string id,  DishDto dishDto, IMediator _mediator) =>
            {
                var result = _mediator.Send(new UpdateDishCommand(id, dishDto));
                return Results.Ok(result);
            }).RequireAuthorization("Admin", "RestaurantOwner");

            app.MapDelete("/api/dishes/{id}", (string id,  IMediator _mediator) =>
            {
                var result = _mediator.Send(new DeleteDishCommand(id));
                return Results.Ok(result);
            }).RequireAuthorization("Admin", "RestaurantOwner");
        }
    }
}
