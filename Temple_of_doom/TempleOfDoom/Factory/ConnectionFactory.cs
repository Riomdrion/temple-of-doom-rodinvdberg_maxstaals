using System.Linq;
using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Factory
{
    public static class ConnectionFactory
    {
        public static void CreateRoomDoors(List<Room> rooms, List<ConnectionDto> connections)
        {
            foreach (var connection in connections)
            {
                // Controleer elke richting en maak verbindingen
                if (connection.NORTH.HasValue)
                    AddConnection(rooms, connection.NORTH.Value, connection, Direction.NORTH);

                if (connection.SOUTH.HasValue)
                    AddConnection(rooms, connection.SOUTH.Value, connection, Direction.SOUTH);

                if (connection.EAST.HasValue)
                    AddConnection(rooms, connection.EAST.Value, connection, Direction.EAST);

                if (connection.WEST.HasValue)
                    AddConnection(rooms, connection.WEST.Value, connection, Direction.WEST);
            }
        }

        private static void AddConnection(List<Room> rooms, int targetRoomId, ConnectionDto connection, Direction direction)
        {
            var currentRoom = rooms.FirstOrDefault(r => r.Id == targetRoomId);
            if (currentRoom == null)
            {
                Console.WriteLine($"Warning: Room with ID {targetRoomId} not found.");
                return;
            }

            // Voeg de deur toe aan de huidige kamer
            foreach (var doorDto in connection.Doors)
            {
                var door = DoorFactory.CreateDoor(doorDto, currentRoom);
                currentRoom.Doors.Add(door);
            }
        }


    }
}