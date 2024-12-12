
using Temple_of_doom.DTO;
using Temple_of_doom.Models;

namespace Temple_of_doom.Factories
{
    public static class DoorFactory
    {
        public static Door CreateDoor(DoorDTO doorDTO)
        {
            return doorDTO.Type switch
            {
                "SimpleDoor" => new SimpleDoor(doorDTO.Id, doorDTO.TargetRoomId),
                "LockedDoor" => new LockedDoor(doorDTO.Id, doorDTO.TargetRoomId),
                "ColoredDoor" => new ColoredDoor(doorDTO.Id, doorDTO.TargetRoomId),
                _ => throw new ArgumentException($"Unknown door type: {doorDTO.Type}")
            };
        }
    }
}
