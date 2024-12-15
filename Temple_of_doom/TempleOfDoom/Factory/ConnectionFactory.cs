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
                foreach (var direction in new[] { Direction.NORTH, Direction.SOUTH, Direction.EAST, Direction.WEST })
                {
                    var targetRoomId = direction switch
                    {
                        Direction.NORTH => connection.south,
                        Direction.SOUTH => connection.north, 
                        Direction.WEST => connection.east, 
                        Direction.EAST => connection.west, 
                        _ => null
                    };

                    if (targetRoomId.HasValue)
                        AddConnection(rooms, targetRoomId.Value, connection, direction);
                }
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
                Direction.NORTH => connection.south,
                Direction.SOUTH => connection.north, 
                Direction.WEST => connection.east, 
                Direction.EAST => connection.west, 
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
                Console.WriteLine($"Added SimpleDoor to Room ID={currentRoom.Id} at {defaultPosition}");
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
