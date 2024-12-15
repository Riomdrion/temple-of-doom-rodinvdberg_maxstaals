using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class ClosingGate : DoorDecorator
{
    public ClosingGate(Door door) : base(door)
    {
        Symbol = (char)Symbols.CLOSINGGATE;
    }

    public override bool CanOpen(Player player)
    {
        return true; // Always allows the player to pass, but closes after.
    }
}
