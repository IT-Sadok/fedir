using booking_imitation_n_layer.BussinesLogic.Models.Domain;
using booking_imitation_n_layer.BussinesLogic.Models.DTO;

namespace booking_imitation_n_layer.BussinesLogic.Services
{
    internal interface IRoomService
    {
        Task<bool> BookRoomAsync(int roomId, DateOnly date);
        Task<List<RoomDTO>> GetAvailableRoomsAsync(DateOnly? date);
        Task<RoomDTO> GetRoomAsync(int id);
        Task<bool> AddRoomAsync(RoomDTO room);
        Task<bool> SaveRoomAsync(RoomDTO room);
        Task<bool> RemoveRoomAsync(int id);
    }
}