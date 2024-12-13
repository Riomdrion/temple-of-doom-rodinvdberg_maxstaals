using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class LockedDoor : Door
{
    public string RequiredKeyColor { get; set; }

    public LockedDoor(int id, int targetRoomId, string requiredKeyColor)
    {
        Id = id;
        TargetRoomId = targetRoomId;
        RequiredKeyColor = requiredKeyColor;
        IsOpen = false;
    }

    public LockedDoor(int doorDtoId, int doorDtoTargetRoomId)
    {
        throw new NotImplementedException();
    }

    public override bool CanOpen(Player player)
    {
        return player.HasKey(RequiredKeyColor);
    }
}