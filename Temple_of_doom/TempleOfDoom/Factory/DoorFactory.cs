using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Door;

namespace TempleOfDoom.Factory;

public class DoorFactory
{
    public static Door CreateDoor(DoorDto dto)
    {
        return dto.Type switch
        {
            "locked" => new LockedDoor(dto.Id, dto.TargetRoomId, dto.KeyColor),
            "colored" => new ColoredDoor(dto.Id, dto.TargetRoomId, dto.KeyColor),
            "toggle" => new ToggleDoor { Id = dto.Id, TargetRoomId = dto.TargetRoomId },
            _ => new SimpleDoor(dto.Id, dto.TargetRoomId)
        };
    }
}
