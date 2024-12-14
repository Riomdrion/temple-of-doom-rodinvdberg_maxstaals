using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Items;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Factory;

public static class RoomFactory
{
    public static List<Room> CreateRooms(List<RoomDto> roomsData)
    {
        var rooms = new List<Room>();

        foreach (var roomData in roomsData)
        {
            // var items = ItemFactory.CreateItems(roomData.Items); // Zet ItemDto's om naar Items
            var doors = new List<Door>(); // Worden later toegevoegd

            Console.WriteLine($"Creating Room: ID={roomData.Id}, Width={roomData.Width}, Height={roomData.Height}");

            var room = new Room(
                id: roomData.Id,
                width: roomData.Width,
                height: roomData.Height,
                items: new List<Item>(),
                doors: doors
            );

            room.InitializeRoomLayout();
            rooms.Add(room);
        }

        return rooms;
    }
}