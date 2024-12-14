using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Door;
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

            if (connection.Doors.Count > 0)
            {
                foreach (var doorDto in connection.Doors)
                {
                    var door = DoorFactory.CreateDoor(doorDto, currentRoom);
                    currentRoom.Doors.Add(door);
                }
            }
            else
            {
                var defaultPosition = CalculateDoorPosition(currentRoom, direction);
                var simpleDoor = new SimpleDoor(currentRoom.Id, targetRoomId, direction, defaultPosition);
                currentRoom.Doors.Add(simpleDoor);
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