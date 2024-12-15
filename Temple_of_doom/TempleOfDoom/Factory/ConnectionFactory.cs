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
                        Direction.NORTH => connection.north,
                        Direction.SOUTH => connection.south,
                        Direction.EAST => connection.east,
                        Direction.WEST => connection.west,
                        _ => null
                    };

                    if (targetRoomId.HasValue)
                        AddConnection(rooms, connection, direction);
                }
            }
        }

        public static void AddConnection(List<Room> rooms, ConnectionDto connection, Direction direction)
        {
            var (currentRoomId, targetRoomId) = direction switch
            {
                Direction.NORTH => (connection.south, connection.north),
                Direction.SOUTH => (connection.north, connection.south),
                Direction.WEST => (connection.east, connection.west),
                Direction.EAST => (connection.west, connection.east),
                _ => (null, null)
            };

            if (currentRoomId.HasValue && targetRoomId.HasValue)
            {
                var currentRoom = rooms.FirstOrDefault(r => r.Id == currentRoomId.Value);
                var targetRoom = rooms.FirstOrDefault(r => r.Id == targetRoomId.Value);

                if (currentRoom != null && targetRoom != null)
                {
                    var doorPosition = DoorFactory.CalculateDoorPosition(currentRoom, direction);
                    var door = new SimpleDoor(currentRoom.Id, targetRoom.Id,direction, doorPosition);

                    currentRoom.Doors.Add(door);
                    Console.WriteLine($"Added door between Room ID={currentRoom.Id} and Room ID={targetRoom.Id}");
                }
                else
                {
                    Console.WriteLine($"Error: Room IDs {currentRoomId} or {targetRoomId} not found!");
                }
            }
        }
    }
}
