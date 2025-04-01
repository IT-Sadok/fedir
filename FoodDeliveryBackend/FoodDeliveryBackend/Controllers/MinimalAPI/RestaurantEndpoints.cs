using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Controllers;
using FoodDeliveryBackend.Domain;
using FoodDeliveryBackend.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryBackend.MinimalAPI
{
    public static class RestaurantEndpoints
    {
        public static void RegisterRestaurantEndpoints(this WebApplication app)
        {
            app.MapGet(ApiRoutes.Restoraunts.GetAll, async (IMediator _mediator) =>
            {
                var result = await _mediator.Send(new GetAllRestaurantsQuery());
                return Results.Ok(result);
            });

            app.MapGet(ApiRoutes.Restoraunts.GetById, async (string id,IMediator _mediator) =>
            {
                var result = await _mediator.Send(new GetRestaurantByIdQuery(id));
                return Results.Ok(result);
            });

            app.MapPost(ApiRoutes.Restoraunts.Create, (RestaurantDto restaurantDto, IMediator _mediator) =>
            {
                var result = _mediator.Send(new CreateRestaurantCommand(restaurantDto));
                return Results.Ok(result);
            }).RequireAuthorization(RoleConstants.Admin, RoleConstants.RestaurantOwner);

            app.MapPut(ApiRoutes.Restoraunts.Update, (string id,  RestaurantDto restaurantDto, IMediator _mediator) =>
            {
                var result = _mediator.Send(new UpdateRestaurantCommand(id, restaurantDto));
                return Results.Ok(result);
            }).RequireAuthorization(RoleConstants.Admin, RoleConstants.RestaurantOwner);

            app.MapDelete(ApiRoutes.Restoraunts.Delete, (string id,  IMediator _mediator) =>
            {
                var result = _mediator.Send(new DeleteRestaurantCommand(id));
                return Results.Ok(result);
            }).RequireAuthorization(RoleConstants.Admin, RoleConstants.RestaurantOwner);
        }
    }
}
