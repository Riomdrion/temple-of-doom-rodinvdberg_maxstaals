using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Factory
{
    public static class ConnectionFactory
    {
        public static List<Connection> CreateConnections(List<Room> rooms, List<dynamic> connectionsData)
        {
            var connections = new List<Connection>();

            foreach (var connectionData in connectionsData)
            {
                // Verkrijg de kamers gebaseerd op het ID
                int? northRoomId = connectionData.NORTH;
                int? southRoomId = connectionData.SOUTH;
                int? eastRoomId = connectionData.EAST;
                int? westRoomId = connectionData.WEST;

                // Maak verbindingen aan voor elke richting
                if (northRoomId.HasValue)
                    connections.Add(CreateConnection(rooms, northRoomId.Value, Direction.NORTH, connectionData.doors));
                if (southRoomId.HasValue)
                    connections.Add(CreateConnection(rooms, southRoomId.Value, Direction.SOUTH, connectionData.doors));
                if (eastRoomId.HasValue)
                    connections.Add(CreateConnection(rooms, eastRoomId.Value, Direction.EAST, connectionData.doors));
                if (westRoomId.HasValue)
                    connections.Add(CreateConnection(rooms, westRoomId.Value, Direction.WEST, connectionData.doors));
            }
            return connections;
        }

        private static Connection CreateConnection(List<Room> rooms, int targetRoomId, Direction direction, dynamic doorsData)
        {
            var targetRoom = rooms.Find(room => room.Id == targetRoomId);

            if (targetRoom == null)
                throw new Exception($"Room with ID {targetRoomId} not found!");

            var connection = new Connection(direction, targetRoom);

            // Voeg deuren toe aan de verbinding
            if (doorsData != null)
            {
                foreach (var doorData in doorsData)
                {
                    DoorDto doorDto = new DoorDto
                    {
                        Id = doorData.id,
                        TargetRoomId = targetRoomId,
                        Type = doorData.type,
                        KeyColor = doorData.color,
                        Direction = direction
                    };

                    var door = DoorFactory.CreateDoor(doorDto, targetRoom);
                    targetRoom.Doors.Add(door);
                }
            }
            return connection;
        }
    }
}