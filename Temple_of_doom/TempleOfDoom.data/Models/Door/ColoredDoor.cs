using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class ColoredDoor : DoorDecorator
{
    private readonly string KeyColor;

    public ColoredDoor(Door door, string keyColor) : base(door)
    {
        KeyColor = keyColor;
    }

    public override bool CanOpen(Player player)
    {
        return player.HasKey(KeyColor) && base.CanOpen(player);
    }
}