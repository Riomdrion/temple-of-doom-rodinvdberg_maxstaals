using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class OpenOnOddDoor : Door
{
    public OpenOnOddDoor(Door baseDoor) : base(baseDoor.Id, baseDoor.TargetRoomId, baseDoor.Direction, baseDoor.Position) { }

    public override bool CanOpen(Player player)
    {
        if (player.Lives % 2 == 0) return false;

        Open();
        return true;
    }
}
