using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class ColoredDoor : Door
{
    public string KeyColor { get; private set; }

    public ColoredDoor(Door baseDoor, string keyColor) : base(baseDoor.Id, baseDoor.TargetRoomId, baseDoor.Direction, baseDoor.Position)
    {
        KeyColor = keyColor;
    }

    public override bool CanOpen(Player player) => player.HasKey(KeyColor);
}