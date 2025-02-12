using booking_imitation_n_layer.BussinesLogic.Models;
using booking_imitation_n_layer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booking_imitation_n_layer.BussinesLogic.Services
{
    internal class RoomService : IRoomService
    {

        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<bool> BookRoomAsync(int roomId, DateOnly date)
        {
            var rooms = await _roomRepository.GetAllAsync();
            var room = rooms.FirstOrDefault(r => r.Id == roomId);
            if (room == null || room.BookedDates.Contains(date)) return false;
            room.BookedDates.Add(date);
            await _roomRepository.SaveAsync(rooms);
            return true;
        }

        public async Task<List<Room>> GetAvailableRoomsAsync()
        {
            DateOnly today = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            var rooms = await _roomRepository.GetAllAsync();
            return rooms.Where(r => !r.BookedDates.Contains(today)).ToList();
        }
    }
}
