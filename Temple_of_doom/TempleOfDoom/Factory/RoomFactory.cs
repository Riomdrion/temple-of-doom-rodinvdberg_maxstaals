using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Items;
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

            // Vul de layout van de kamer met standaardwaarden
            InitializeRoomLayout(room);

            rooms.Add(room);
        }

        return rooms;
    }

    // private static List<Item> CreateItems(dynamic itemsData)
    // {
    //     var items = new List<Item>();
    //
    //     if (itemsData == null) return items;
    //
    //     foreach (var itemData in itemsData)
    //     {
    //         var item = itemData.type switch
    //         {
    //             "sankara stone" => new Item("Sankara Stone", itemData.x, itemData.y),
    //             "key" => new Item($"Key ({itemData.color})", itemData.x, itemData.y),
    //             "boobytrap" => new Item("Boobytrap", itemData.x, itemData.y),
    //             "disappearing boobytrap" => new Item("Disappearing Boobytrap", itemData.x, itemData.y),
    //             "pressure plate" => new Item("Pressure Plate", itemData.x, itemData.y),
    //             _ => null
    //         };
    //
    //         if (item != null) items.Add(item);
    //     }
    //
    //     return items;
    // }items

    private static void InitializeRoomLayout(Room room)
    {
        for (var y = 0; y < room.Height; y++)
        {
            for (var x = 0; x < room.Width; x++)
            {
                if (y == 0 || y == room.Height - 1 || x == 0 || x == room.Width - 1)
                {
                    room.Layout[y, x] = '#'; // Randen
                }
                else
                {
                    room.Layout[y, x] = ' '; // Lege ruimte
                }
            }
        }

        // Plaats items in de kamer
        // foreach (var item in room.Items)
        // {
        //     room.Layout[item.Y, item.X] = item.Name switch
        //     {
        //         "Sankara Stone" => (char)Symbols.SANKARASTONE,
        //         "Key (Red)" => (char)Symbols.KEY,
        //         "Key (Green)" => (char)Symbols.KEY,
        //         "Boobytrap" => (char)Symbols.BOOBYTRAP,
        //         "Disappearing Boobytrap" => (char)Symbols.DISSAPINGBOOBYTRAP,
        //         "Pressure Plate" => (char)Symbols.PRESSUREPLATE,
        //         _ => ' '
        //     };
        // }
    }
}