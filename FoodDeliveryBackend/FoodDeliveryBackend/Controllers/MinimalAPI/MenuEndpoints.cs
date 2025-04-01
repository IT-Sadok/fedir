using FoodDeliveryBackend.Application.CQRS.Commands;
using FoodDeliveryBackend.Application.CQRS.Queries;
using FoodDeliveryBackend.Controllers;
using FoodDeliveryBackend.Domain;
using FoodDeliveryBackend.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryBackend.MinimalAPI
{
    public static class MenusEndpoints
    {
        public static void RegisterMenusEndpoints(this WebApplication app)
        {
            app.MapGet(ApiRoutes.Menues.GetAll, async (IMediator _mediator) =>
            {
                var result = await _mediator.Send(new GetAllMenusQuery());
                return Results.Ok(result);
            });

            app.MapGet(ApiRoutes.Menues.GetById, async (string id,IMediator _mediator) =>
            {
                var result = await _mediator.Send(new GetMenuByIdQuery(id));
                return Results.Ok(result);
            });

            app.MapPost(ApiRoutes.Menues.Create, (MenuDto menuDto, IMediator _mediator) =>
            {
                var result = _mediator.Send(new CreateMenuCommand(menuDto));
                return Results.Ok(result);
            }).RequireAuthorization(RoleConstants.Admin, RoleConstants.RestaurantOwner);

            app.MapPut(ApiRoutes.Menues.Update, (string id,  MenuDto menuDto, IMediator _mediator) =>
            {
                var result = _mediator.Send(new UpdateMenuCommand(id, menuDto));
                return Results.Ok(result);
            }).RequireAuthorization(RoleConstants.Admin, RoleConstants.RestaurantOwner);

            app.MapDelete(ApiRoutes.Menues.Delete, (string id,  IMediator _mediator) =>
            {
                var result = _mediator.Send(new DeleteMenuCommand(id));
                return Results.Ok(result);
            }).RequireAuthorization(RoleConstants.Admin, RoleConstants.RestaurantOwner);
        }
    }
}
