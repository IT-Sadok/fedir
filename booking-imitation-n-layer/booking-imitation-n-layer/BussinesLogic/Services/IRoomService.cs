using booking_imitation_n_layer.BussinesLogic.Models;

namespace booking_imitation_n_layer.BussinesLogic.Services
{
    internal interface IRoomService
    {
        Task<bool> BookRoomAsync(int roomId, DateOnly date);
        Task<List<Room>> GetAvailableRoomsAsync();
    }
}