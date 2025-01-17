using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class ClosingGate : DoorDecorator
{
    private bool _isActivated;
    private bool _canOpen;
    public ClosingGate(Door door) : base(door)
    {
        Symbol = (char)Symbols.CLOSINGGATE;
        _isActivated = false;
        _canOpen = true;
    }

    public override bool CanOpen(Player player)
    {
        if (_isActivated == false)
        {
            _isActivated = true;
            _canOpen = false;
        }
        return _canOpen;
    }
}
