using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Factory;

public static class DoorFactory
{
    public static Door CreateDoor(DoorDto dto, Room room)
    {
        var position = CalculateDoorPosition(room, dto.Direction);
        Door baseDoor = new SimpleDoor(dto.Id, dto.TargetRoomId, dto.Direction, position);

        return dto.Type switch
        {
            "colored" => new ColoredDoor(baseDoor, dto.KeyColor),
            "toggle" => new ToggleDoor(baseDoor),
            "closing gate" => new ClosingGate(baseDoor),
            "open on odd" => new OpenOnOddDoor(baseDoor),
            "open on stones in room" => new OpenOnStonesDoor(baseDoor, dto.RequiredStones),
            _ => baseDoor
        };
    }

    private static Position CalculateDoorPosition(Room room, Direction direction)
    {
        return direction switch
        {
            Direction.NORTH => new Position(room.Width / 2, 0), // Midden van de noordmuur
            Direction.SOUTH => new Position(room.Width / 2, room.Height - 1), // Midden van de zuidmuur
            Direction.WEST => new Position(0, room.Height / 2), // Midden van de westmuur
            Direction.EAST => new Position(room.Width - 1, room.Height / 2), // Midden van de oostmuur
            _ => throw new Exception("Invalid direction")
        };
    }
}

