using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Factory
{
    public static class ConnectionFactory
    {
        public static List<Connection> CreateRoomDoors(List<Room> rooms, List<RoomDto> roomDtos)
        {
            var connections = new List<Connection>();

            foreach (var roomDto in roomDtos)
            {
                foreach (var doorDto in roomDto.Doors)
                {
                    var targetRoom = rooms.FirstOrDefault(r => r.Id == doorDto.TargetRoomId);
                    if (targetRoom == null)
                        throw new Exception($"Target room with ID {doorDto.TargetRoomId} not found!");

                    var connection = new Connection(Enum.Parse<Direction>(doorDto.Direction.ToString(), true), targetRoom);
                    connections.Add(connection);
                }
            }

            return connections;
        }

        private static Connection CreateConnection(List<Room> rooms, DoorDto doorDto)
        {
            // Zoek de target room
            var targetRoom = rooms.FirstOrDefault(room => room.Id == doorDto.TargetRoomId);
            if (targetRoom == null)
                throw new Exception($"Room with ID {doorDto.TargetRoomId} not found!");

            // Maak een nieuwe Connection
            var connection = new Connection(Enum.Parse<Direction>(doorDto.Direction.ToString(), true), targetRoom);

            // Voeg deuren toe aan de target room
            var door = DoorFactory.CreateDoor(doorDto, targetRoom);
            targetRoom.Doors.Add(door);

            return connection;
        }
    }
}