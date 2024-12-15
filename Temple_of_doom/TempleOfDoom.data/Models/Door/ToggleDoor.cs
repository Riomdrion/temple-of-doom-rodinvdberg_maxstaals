using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class ToggleDoor : Door
{
    public ToggleDoor(Door baseDoor) : base(baseDoor.Id, baseDoor.TargetRoomId, baseDoor.Direction, baseDoor.Position)
    {
    }

    public override bool CanOpen(Player player) => IsOpen;
}