using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.FloorTiles;

public class IceTile(Position position) : FloorTile(position)
{
    public override void ApplyEffect(Player player, Room room)
    {
        throw new NotImplementedException();
    }
}