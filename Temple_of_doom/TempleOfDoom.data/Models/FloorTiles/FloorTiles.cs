using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.FloorTiles;

public abstract class FloorTile(Position position)
{
    public char Symbol { get; protected set; } = (char)Symbols.ICEFLOORTILE;
    public Position position { get; set; } = position;

    public abstract void ApplyEffect(Player player, Room room);
}