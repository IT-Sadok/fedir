using booking_imitation_n_layer.BussinesLogic.Models;
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
        Task<List<Room>> GetAllFreeAsync();
        Task GetAsync(string roomId);
        Task SaveAsync(List<Room> rooms);
    }
}
