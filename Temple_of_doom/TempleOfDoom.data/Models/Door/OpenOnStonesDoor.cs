using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class OpenOnStonesDoor : Door
{
    private readonly int _requiredStones;

    public OpenOnStonesDoor(Door baseDoor, int requiredStones) : base(baseDoor.Id, baseDoor.TargetRoomId, baseDoor.Direction, baseDoor.Position)
    {
        _requiredStones = requiredStones;
    }

    public override bool CanOpen(Player player)
    {
        // var stonesInRoom = player.CurrentRoom.Items.Count(item => item.Type == "sankara stone");
        // return stonesInRoom == _requiredStones && base.CanOpen(player);
        return false;
    }
}
