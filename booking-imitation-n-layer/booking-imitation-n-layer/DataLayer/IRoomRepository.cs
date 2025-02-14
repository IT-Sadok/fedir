using booking_imitation_n_layer.BussinesLogic.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booking_imitation_n_layer.DataLayer
{
    internal interface IRoomRepository
    {
        Task<List<Room>> GetAllAsync();
        Task SaveAsync(List<Room> rooms);
        Task<List<Room>> GetFreeOnDateAsync(DateOnly date);
        Task<Room> GetRoomAsync(int roomId);
        Task<bool> AddRoomAsync(Room room);
        Task<bool> SaveRoomAsync(Room room);
        Task<bool> RemoveRoomAsync(int id);
    }
}
