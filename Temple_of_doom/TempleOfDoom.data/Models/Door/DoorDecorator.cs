using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public abstract class DoorDecorator : Door
{
    protected Door door;
    
    public override bool CanOpen(Player player)
    {
        return door.CanOpen(player);
    }
}