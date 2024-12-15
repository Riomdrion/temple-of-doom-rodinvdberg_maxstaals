using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class SimpleDoor : Door
{
    public SimpleDoor(int id, int targetRoomId, Direction direction, Position position)
        : base(id, targetRoomId, direction, position) { }

    public override bool CanOpen(Player player)
    {
        return true; 
    }
}
