using AutoMapper;
using booking_imitation_n_layer.BussinesLogic.Models.Domain;
using booking_imitation_n_layer.BussinesLogic.Models.DTO;
using booking_imitation_n_layer.DataLayer;
using System.Linq;

namespace booking_imitation_n_layer.BussinesLogic.Services
{
    internal class RoomService : IRoomService
    {

        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
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

        public async Task<List<RoomDTO>> GetAvailableRoomsAsync(DateOnly? date)
        {
            if (date is null)
            {
                date = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            }
            var rooms = await _roomRepository.GetAllAsync();
            var result = _mapper.Map<List<Room>, List<RoomDTO>>(rooms).Where(r => !r.BookedDates.Contains(date.Value)).ToList();
            return result;
        }

        public async Task<RoomDTO> GetRoomAsync(int id)
        {
            var rooms = await _roomRepository.GetAllAsync();
            return _mapper.Map<List<Room>, List<RoomDTO>>(rooms).First(r => r.Id == id);
        }

        public async Task<bool> AddRoomAsync(RoomDTO room)
        {
            var result = await _roomRepository.AddRoomAsync(_mapper.Map<RoomDTO, Room>(room));
            return result;
        }

        public async Task<bool> SaveRoomAsync(RoomDTO room)
        {
            var result = await _roomRepository.SaveRoomAsync(_mapper.Map<RoomDTO, Room>(room));
            return result;
        }

        public async Task<bool> RemoveRoomAsync(int id)
        {
            var result = await _roomRepository.RemoveRoomAsync(id);
            return result;
        }
    }
}
