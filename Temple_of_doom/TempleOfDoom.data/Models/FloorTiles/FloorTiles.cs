using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.FloorTiles;

public abstract class FloorTile(Position position)
{
    private Position position { get; set; } = position;

    public abstract void ApplyEffect(Player player, Room room);
}