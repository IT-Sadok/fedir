
using booking_imitation_n_layer.BussinesLogic.Services;
using booking_imitation_n_layer.DataLayer;
using booking_imitation_n_layer.Presentation;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IRoomService, RoomService>()
    .AddSingleton<IRoomRepository, RoomRepository>()
    .BuildServiceProvider();

PresentationLayer presentationLayer = new PresentationLayer(serviceProvider.GetService<IRoomService>());

await presentationLayer.RunUi();