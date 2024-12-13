namespace TempleOfDoom.data.Models.Map;

public class Connection
{
    public Connection(Direction direction, Room connectedRoom)
    {
        Direction = direction;
        ConnectedRoom = connectedRoom;
    }

    public Direction Direction { get; set; }
    public Room ConnectedRoom { get; set; }
}