using Temple_of_doom.DTO;
using Temple_of_doom.Factories;
using Temple_of_doom.Models;

namespace Temple_of_doom.Mappers
{
    public class RoomMapper
    {
        public Room MapToRoom(RoomDTO roomDTO)
        {
            // Gebruik een factory pattern om afgeleide klassen van Item en Door te genereren
            var room = new Room(roomDTO.Id, roomDTO.Name, roomDTO.Width, roomDTO.Height,
                roomDTO.Items?.Select(itemDto => ItemFactory.CreateItem(itemDto)).ToList() ?? new List<Item>(),
                roomDTO.Doors?.Select(doorDto => DoorFactory.CreateDoor(doorDto)).ToList() ?? new List<Door>()
            );

            room.Layout = GenerateLayout(room.Width, room.Height);
            return room;
        }

        public RoomDTO MapToRoomDTO(Room room)
        {
            return new RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                Width = room.Width,
                Height = room.Height,
                Items = room.Items.Select(item => new ItemDTO
                {
                    Name = item.Name,
                    Type = item.GetType().Name
                }).ToList(),
                Doors = room.Doors.Select(door => new DoorDTO
                {
                    Id = door.Id,
                    TargetRoomId = door.TargetRoomId
                }).ToList()
            };
        }

        private char[,] GenerateLayout(int width, int height)
        {
            char[,] layout = new char[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    layout[y, x] = '.'; // Default to empty space
                }
            }

            // Add walls
            for (int x = 0; x < width; x++)
            {
                layout[0, x] = '#';
                layout[height - 1, x] = '#';
            }

            for (int y = 0; y < height; y++)
            {
                layout[y, 0] = '#';
                layout[y, width - 1] = '#';
            }

            return layout;
        }
    }
}