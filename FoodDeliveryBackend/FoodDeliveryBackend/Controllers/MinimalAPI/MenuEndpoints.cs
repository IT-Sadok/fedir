using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryBackend.MinimalAPI
{
    public static class MenusEndpoints
    {
        public static void RegisterMenusEndpoints(this WebApplication app)
        {
            app.MapGet("/api/menus", async (IMediator _mediator) =>
            {
                var result = await _mediator.Send(new GetAllMenusQuery());
                return Results.Ok(result);
            });

            app.MapGet("/api/menus/{id}", async (string id,IMediator _mediator) =>
            {
                var result = await _mediator.Send(new GetMenuByIdQuery(id));
                return Results.Ok(result);
            });

            app.MapPost("/api/menus", (MenuDto menuDto, IMediator _mediator) =>
            {
                var result = _mediator.Send(new CreateMenuCommand(menuDto));
                return Results.Ok(result);
            }).RequireAuthorization("Admin", "RestaurantOwner");

            app.MapPut("/api/menus/{id}", (string id,  MenuDto menuDto, IMediator _mediator) =>
            {
                var result = _mediator.Send(new UpdateMenuCommand(id, menuDto));
                return Results.Ok(result);
            }).RequireAuthorization("Admin", "RestaurantOwner");

            app.MapDelete("/api/menus/{id}", (string id,  IMediator _mediator) =>
            {
                var result = _mediator.Send(new DeleteMenuCommand(id));
                return Results.Ok(result);
            }).RequireAuthorization("Admin", "RestaurantOwner");
        }
    }
}
