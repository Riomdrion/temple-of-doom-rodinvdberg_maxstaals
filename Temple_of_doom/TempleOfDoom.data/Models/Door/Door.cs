using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public abstract class Door : UiObserver
{
    public int Id { get; set; }
    public bool IsOpen { get; set; }
    public int TargetRoomId { get; set; }

    public abstract bool CanOpen(Player player);

    public void Open()
    {
        IsOpen = true;
    }

    public void Close()
    {
        IsOpen = false;
    }
}