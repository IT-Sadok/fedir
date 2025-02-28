using booking_imitation_n_layer.BussinesLogic.Models.DTO;
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
                Console.WriteLine("Choose mode: " +
                    "\nU key for user mode" +
                    "\nA key for admin mode" +
                    "\nEsc - Exit");
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
                Console.WriteLine("Esc - Exit");
                Console.Write("\nChoose an option: ");
                var input = Console.ReadKey().Key;
                Console.Write("\n");

                switch (input)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        var rooms = await roomService.GetAvailableRoomsAsync(null);
                        foreach (var room in rooms)
                            Console.WriteLine($"Room {room.Id}");
                        break;
                    case ConsoleKey.D2:
                        Console.Write("Enter Room ID to add: ");
                        int.TryParse(Console.ReadLine(), out int addRoomId);

                        var newRoom = new RoomDTO() { Id = addRoomId };

                        bool created = await roomService.AddRoomAsync(newRoom);
                        Console.WriteLine(created ? "Room created successfully!" : "Creating failed!");
                        break;
                    case ConsoleKey.D3:
                        Console.Write("Enter Room ID to remove: ");
                        int.TryParse(Console.ReadLine(), out int removeRoomId);

                        bool removed = await roomService.RemoveRoomAsync(removeRoomId);
                        Console.WriteLine(removed ? "Room removed successfully!" : "Removing failed!");
                        break;
                    case ConsoleKey.Escape:
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
                Console.WriteLine("1. Show Available Rooms on date");
                Console.WriteLine("2. Book a Room");
                Console.WriteLine("3. Edit book");
                Console.WriteLine("Esc - Exit");
                Console.Write("\nChoose an option: ");
                var input = Console.ReadKey().Key;
                Console.Write("\n");

                switch (input)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Enter date (DD/MM/YYYY): ");
                        DateOnly.TryParse(Console.ReadLine(), out DateOnly avaluableOnDate);
                        var rooms = await roomService.GetAvailableRoomsAsync(avaluableOnDate);
                        foreach (var room in rooms)
                            Console.WriteLine($"Room {room.Id}");
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("Enter Room ID to Book: ");
                        int.TryParse(Console.ReadLine(), out int roomId);
                        Console.WriteLine("Enter date (DD/MM/YYYY): ");
                        DateOnly.TryParse(Console.ReadLine(), out DateOnly date);
                        bool booked = await roomService.BookRoomAsync(roomId, date);
                        Console.WriteLine(booked ? "Room booked successfully!" : "Booking failed!");
                        break;
                    case ConsoleKey.D3:
                        Console.Write("Enter Room ID to edit: ");
                        int.TryParse(Console.ReadLine(), out int roomToEditId);
                        var roomToEdit = await roomService.GetRoomAsync(roomToEditId);
                        Console.WriteLine($"Room book date: {roomToEdit.BookedDates[0]}");
                        Console.WriteLine("Enter new book date: ");
                        DateOnly.TryParse(Console.ReadLine(), out DateOnly newBookDate);
                        roomToEdit.BookedDates[0] = newBookDate;
                        bool isSavedEdit = await roomService.SaveRoomAsync(roomToEdit);
                        Console.WriteLine(isSavedEdit ? "Room change saved successfully!" : "Saving failed failed!");
                        break;
                    case ConsoleKey.Escape:
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
