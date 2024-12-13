using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class ColoredDoor : Door
{
    public string KeyColor { get; set; }

    public ColoredDoor(int id, int targetRoomId, string keyColor)
    {
        Id = id;
        TargetRoomId = targetRoomId;
        KeyColor = keyColor;
        IsOpen = false;
    }

    public ColoredDoor(int doorDtoId, int doorDtoTargetRoomId)
    {
        throw new NotImplementedException();
    }

    public override bool CanOpen(Player player)
    {
        return player.HasKey(KeyColor);
    }
}