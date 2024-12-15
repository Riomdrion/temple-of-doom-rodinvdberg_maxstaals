using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class ClosingGate : Door
{
    public ClosingGate(Door baseDoor) : base(baseDoor.Id, baseDoor.TargetRoomId, baseDoor.Direction, baseDoor.Position)
    {
        Symbol = (char)Symbols.CLOSINGGATE;
    }

    
    public override bool CanOpen(Player player)
    {

        // if (!IsOpen) return false;
        //
        // Close();
        return true;
    }
}
