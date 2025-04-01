using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Controllers;
using FoodDeliveryBackend.Domain;
using FoodDeliveryBackend.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryBackend.MinimalAPI
{
    public static class DishEndpoints
    {
        public static void RegisterDishEndpoints(this WebApplication app)
        {
            app.MapGet(ApiRoutes.Dishes.GetAll, async (IMediator _mediator) =>
            {
                var result = await _mediator.Send(new GetAllDishesQuery());
                return Results.Ok(result);
            });

            app.MapGet(ApiRoutes.Dishes.GetById, async (string id,IMediator _mediator) =>
            {
                var result = await _mediator.Send(new GetDishByIdQuery(id));
                return Results.Ok(result);
            });

            app.MapPost(ApiRoutes.Dishes.Create, (DishDto dishDto, IMediator _mediator) =>
            {
                var result = _mediator.Send(new CreateDishCommand(dishDto));
                return Results.Ok(result);
            }).RequireAuthorization(RoleConstants.Admin, RoleConstants.RestaurantOwner);

            app.MapPut(ApiRoutes.Dishes.Update, (string id,  DishDto dishDto, IMediator _mediator) =>
            {
                var result = _mediator.Send(new UpdateDishCommand(id, dishDto));
                return Results.Ok(result);
            }).RequireAuthorization(RoleConstants.Admin, RoleConstants.RestaurantOwner);

            app.MapDelete(ApiRoutes.Dishes.Delete, (string id,  IMediator _mediator) =>
            {
                var result = _mediator.Send(new DeleteDishCommand(id));
                return Results.Ok(result);
            }).RequireAuthorization(RoleConstants.Admin, RoleConstants.RestaurantOwner);
        }
    }
}
