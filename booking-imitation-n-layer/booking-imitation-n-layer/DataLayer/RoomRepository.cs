using booking_imitation_n_layer.BussinesLogic.Models;
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
                var initial = new List<Room>()
                { new()
                {
                    Id = 1,
                }, new()
                {
                    Id = 2,
                }, new()
                {
                    Id = 3,
                }, new()
                {
                    Id = 4,
                }, new()
                {
                    Id = 5,
                }, new()
                {
                    Id = 6,
                }, new()
                {
                    Id = 7,
                }, new()
                {
                    Id = 8,
                }, new()
                {
                    Id = 9,
                }, new()
                {
                    Id = 10,
                }, new()
                {
                    Id = 11,
                }, new()
                {
                    Id = 12,
                }, new()
                {
                    Id = 13,
                }, new()
                {
                    Id = 14,
                }, new()
                {
                    Id = 15,
                }, new()
                {
                    Id = 16,
                }
                };
                var json = JsonSerializer.Serialize(initial);
                File.WriteAllText(_filePath, json);
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
            var json = JsonSerializer.Serialize(rooms);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<List<Room>> GetFreeOnDateAsync(DateOnly date)
        {
            var allRooms = await GetAllAsync();

            return allRooms.Where(_ => !_.BookedDates.Contains(date)).ToList();
        }

        public async Task<List<Room>> GetAllFreeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task GetAsync(string roomId)
        {
            throw new NotImplementedException();
        }
    }
}
