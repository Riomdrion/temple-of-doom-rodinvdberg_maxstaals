using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public abstract class Door : UiObserver
{
    public int Id { get; set; }
    public bool IsOpen { get; set; }
    public int TargetRoomId { get; set; }
    public Position Position { get; private set; }
    public Direction Direction { get; private set; }

    public Door(int id, int targetRoomId, Direction direction, Position position)
    {
        Id = id;
        TargetRoomId = targetRoomId;
        Direction = direction;
        Position = position;
        IsOpen = false;
    }

    public abstract bool CanOpen(Player player);

    public void Open() => IsOpen = true;

    public void Close() => IsOpen = false;
}
