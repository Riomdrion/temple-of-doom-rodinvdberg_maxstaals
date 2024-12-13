using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class ToggleDoor : Door
{
    public override bool CanOpen(Player player)
    {
        return true;
    }

    public void Open()
    {
        throw new NotImplementedException("Open not implemented for ToggleDoor");
    }
}