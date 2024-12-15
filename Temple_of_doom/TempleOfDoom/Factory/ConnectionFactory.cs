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
                foreach (var door in connection.Doors)
                {
                    Console.WriteLine($"Parsed DoorDto: ID={door.Id}, TargetRoomId={door.TargetRoomId}, Type={door.Type}, Direction={door.Direction}");
                }
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
                    {
                        // Console.WriteLine($"Connection: {connection}, Direction: {direction}, TargetRoomId: {targetRoomId}");
                        AddConnection(rooms, targetRoomId.Value, connection, direction);
                    }
                }
            }
        }

        private static void AddConnection(List<Room> rooms, int targetRoomId, ConnectionDto connection, Direction direction)
        {
            Console.WriteLine($"fafaasdasdaAddConnection: targetRoomId={targetRoomId}, connection={connection}, direction={direction}");
            Room? currentRoom = null;
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].Id == targetRoomId)
                {
                    currentRoom = rooms[i];
                    break;
                }
            }
            
            if (currentRoom == null)
            {
                Console.WriteLine($"Warning: Room with ID {targetRoomId} not found.");
                return;
            }

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

            if (connection.Doors.Count == 0)
            {
                var defaultPosition = DoorFactory.CalculateDoorPosition(currentRoom, direction);
                var simpleDoor = new SimpleDoor(currentRoom.Id, connectedRoomId.Value, direction, defaultPosition);
                currentRoom.Doors.Add(simpleDoor);
                return;
            }

            foreach (var doorDto in connection.Doors)
            {
                Console.WriteLine($"DoorDto: ID={doorDto.Id}, TargetRoomId={doorDto.TargetRoomId}, Type={doorDto.Type}, Direction={doorDto.Direction}");
                var door = DoorFactory.CreateDoor(doorDto, currentRoom);
                door.TargetRoomId = connectedRoomId.Value;
                currentRoom.Doors.Add(door);
                Console.WriteLine($"DoorDto: ID={doorDto.Id}, TargetRoomId={doorDto.TargetRoomId}, Type={doorDto.Type}, Direction={doorDto.Direction}");

            }
        }
    }
}
