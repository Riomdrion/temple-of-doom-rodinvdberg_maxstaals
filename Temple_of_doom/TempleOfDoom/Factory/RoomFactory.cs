using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Factory;

public static class RoomFactory
{
    public static List<Room> CreateRooms(List<dynamic> roomsData)
    {
        var rooms = new List<Room>();

        foreach (var roomData in roomsData)
        {
            var room = new Room(
                id: roomData.id,
                name: $"Room {roomData.id}",
                width: roomData.width,
                height: roomData.height,
                items: null,
                doors: new List<Door>()
            );
            
            room.Doors = ConnectionFactory.CreateRoomDoors(rooms, roomData);
            // Vul de layout van de kamer met standaardwaarden
            room.InitializeRoomLayout();
            rooms.Add(room);
        }

        return rooms;
    }
}