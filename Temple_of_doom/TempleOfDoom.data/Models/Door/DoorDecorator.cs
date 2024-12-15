using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public abstract class DoorDecorator : Door
{
    protected Door Door;

    protected DoorDecorator(Door door) : base(door.Id, door.TargetRoomId, door.Direction, door.Position)
    {
        Door = door;
    }

    public override bool CanOpen(Player player)
    {
        return Door.CanOpen(player); // Doorlogica van de gedelegeerde deur
    }
}
