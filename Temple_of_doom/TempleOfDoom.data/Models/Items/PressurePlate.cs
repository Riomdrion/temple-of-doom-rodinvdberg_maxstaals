using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Items;

public class PressurePlate : Item
{
    public PressurePlate(int x, int y) : base(x, y, "pressure plate")
    {
    }
    
    public void StepOn(Room currentRoom)
    {
        foreach (var door in currentRoom.Doors.OfType<ToggleDoor>())
        {
            door.Toggle();
        }
    }
}