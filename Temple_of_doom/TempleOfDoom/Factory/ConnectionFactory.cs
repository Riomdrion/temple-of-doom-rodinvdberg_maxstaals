using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Map;
using TempleOfDoom.ui;

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

        private static void AddConnection(List<Room> rooms, int targetRoomId, ConnectionDto connection,
            Direction direction)
        {
            var currentRoom = rooms.FirstOrDefault(r => r.Id == targetRoomId);
            if (currentRoom == null)
            {
                Console.WriteLine($"Warning: Room with ID {targetRoomId} not found.");
                return;
            }

            // Bepaal het verbonden kamer ID op basis van de richting
            int? connectedRoomId = direction switch
            {
                Direction.NORTH => connection.SOUTH, // Verbinding naar het zuiden in de verbonden kamer
                Direction.SOUTH => connection.NORTH, // Verbinding naar het noorden in de verbonden kamer
                Direction.WEST => connection.EAST, // Verbinding naar het oosten in de verbonden kamer
                Direction.EAST => connection.WEST, // Verbinding naar het westen in de verbonden kamer
                _ => null
            };

            if (!connectedRoomId.HasValue)
            {
                Console.WriteLine($"No connected room for Room ID={targetRoomId} in direction={direction}");
                return;
            }

            // Voeg een SimpleDoor toe als er geen deuren zijn
            if (connection.Doors.Count == 0)
            {
                var defaultPosition = CalculateDoorPosition(currentRoom, direction);
                var simpleDoor = new SimpleDoor(currentRoom.Id, connectedRoomId.Value, direction, defaultPosition);
                currentRoom.Doors.Add(simpleDoor);
                return;
            }

            // Verwerk expliciete deuren
            foreach (var doorDto in connection.Doors)
            {
                var door = DoorFactory.CreateDoor(doorDto, currentRoom);
                door.TargetRoomId = connectedRoomId.Value;
                currentRoom.Doors.Add(door);
            }
        }

        private static Position CalculateDoorPosition(Room room, Direction direction)
        {
            return direction switch
            {
                Direction.NORTH => new Position(room.Width / 2, 0), // Midden van de noordmuur
                Direction.SOUTH => new Position(room.Width / 2, room.Height - 1), // Midden van de zuidmuur
                Direction.EAST => new Position(room.Width - 1, room.Height / 2), // Midden van de oostmuur
                Direction.WEST => new Position(0, room.Height / 2), // Midden van de westmuur
                _ => throw new ArgumentException("Invalid direction")
            };
        }
    }
}