using booking_imitation_n_layer.BussinesLogic.Services;
using booking_imitation_n_layer.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booking_imitation_n_layer.Presentation
{
    internal class PresentationLayer
    {
        private IRoomService roomService;

        public PresentationLayer(IRoomService roomService): base() {
            this.roomService = roomService;
        }

        public async Task RunUi()
        {
            ConsoleKey choosenMode = new ConsoleKey();
            while (choosenMode != ConsoleKey.A || choosenMode != ConsoleKey.U)
            {
                Console.Clear();
                Console.WriteLine("Choose mode: \nU key for user mode\nA key for admin mode");
                choosenMode = Console.ReadKey().Key;

                switch (choosenMode)
                {
                    case ConsoleKey.U:
                        await RunUserUi();
                        break;
                    case ConsoleKey.A:
                        await RunAdminUi();
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }

        private async Task RunAdminUi()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Admin panel");
                Console.WriteLine("1. Show all Rooms");
                Console.WriteLine("2. Add a Room");
                Console.WriteLine("3. Remove a Room");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                var input = Console.ReadKey().Key;

                switch (input)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        var rooms = await roomService.GetAvailableRoomsAsync();
                        foreach (var room in rooms)
                            Console.WriteLine($"Room {room.Id}");
                        break;
                    case ConsoleKey.D2:
                        Console.Write("Enter Room ID to Book: ");
                        int.TryParse(Console.ReadLine(), out int roomId);

                        Console.Write("Enter date (DD/MM/YYYY): ");
                        DateOnly.TryParse(Console.ReadLine(), out DateOnly date);

                        bool booked = await roomService.BookRoomAsync(roomId, date);
                        Console.WriteLine(booked ? "Room booked successfully!" : "Booking failed!");
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

                Console.ReadKey();
            }
        }

        private async Task RunUserUi()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Booking.room");
                Console.WriteLine("1. Show Available Rooms");
                Console.WriteLine("2. Book a Room");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                var input = Console.ReadKey().Key;

                switch (input)
                {
                    case ConsoleKey.D1:
                        var rooms = await roomService.GetAvailableRoomsAsync();
                        foreach (var room in rooms)
                            Console.WriteLine($"Room {room.Id}");
                        break;
                    case ConsoleKey.D2:
                        Console.Write("Enter Room ID to Book: ");
                        int.TryParse(Console.ReadLine(), out int roomId);
                        Console.Write("Enter date (DD/MM/YYYY): ");
                        DateOnly.TryParse(Console.ReadLine(), out DateOnly date);
                        bool booked = await roomService.BookRoomAsync(roomId, date);
                        Console.WriteLine(booked ? "Room booked successfully!" : "Booking failed!");
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

                Console.ReadKey();
            }
        }

    }
}
