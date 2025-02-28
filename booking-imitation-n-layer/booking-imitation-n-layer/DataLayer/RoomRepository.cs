using booking_imitation_n_layer.BussinesLogic.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace booking_imitation_n_layer.DataLayer
{
    class RoomRepository : IRoomRepository
    {
        private readonly string _filePath = "rooms.json";

        public RoomRepository()
        {
            if (!File.Exists(_filePath))
            {
                InitializeDBFile();
            }
        }

        public async Task<List<Room>> GetAllAsync()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var json = await File.ReadAllTextAsync(_filePath);
            var result = JsonSerializer.Deserialize<List<Room>>(json, options);
            return result;
        }

        public async Task SaveAsync(List<Room> rooms)
        {
            rooms = rooms.OrderBy(r => r.Id).ToList();
            var json = JsonSerializer.Serialize(rooms);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<List<Room>> GetFreeOnDateAsync(DateOnly date)
        {
            var allRooms = await GetAllAsync();

            return allRooms.Where(_ => !_.BookedDates.Contains(date)).ToList();
        }

        public async Task<Room> GetRoomAsync(int roomId)
        {
            var allRooms = await GetAllAsync();
            return allRooms.First(_ => _.Id == roomId);
        }

        public async Task<bool> AddRoomAsync(Room room)
        {
            var allRooms = await GetAllAsync();
            allRooms.Add(room);
            try
            {
                await SaveAsync(allRooms);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> SaveRoomAsync(Room room)
        {
            var allRooms = await GetAllAsync();
            var oldRoom = allRooms.First(r => r.Id == room.Id);
            oldRoom = room;
            oldRoom.BookedDates = room.BookedDates;
            try
            {
                await SaveAsync(allRooms);
            }
            catch(Exception e)
            {
                return false;
            }
                
            return true;
        }

        public async Task<bool> RemoveRoomAsync(int id)
        {
            var allRooms = await GetAllAsync();
            allRooms.Remove(allRooms.First(_ => _.Id == id));
            
            try
            {
                await SaveAsync(allRooms);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        private void InitializeDBFile()
        {
            var initial = new List<Room>
                {
                    new Room { Id = 1},
                    new Room { Id = 2},
                    new Room {Id = 3 },
                    new Room {Id = 4},
                    new Room {Id = 5},
                    new Room {Id = 6},
                    new Room {Id = 7},
                    new Room {Id = 8},
                    new Room {Id = 9},
                    new Room {Id = 10},
                    new Room {Id = 11},
                    new Room {Id = 12},
                    new Room {Id = 13},
                    new Room {Id = 14},
                    new Room {Id = 15},
                    new Room {Id = 16},
                };

            var json = JsonSerializer.Serialize(initial);
            File.WriteAllText(_filePath, json);
            Console.WriteLine("Database seeded with default rooms.");

        }
    }
}
