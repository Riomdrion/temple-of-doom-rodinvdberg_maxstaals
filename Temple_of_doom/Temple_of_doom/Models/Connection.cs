namespace Temple_of_doom.Models
{
    public class Connection
    {
        public Direction Direction { get; set; }
        public Room ConnectedRoom { get; set; }

        public Connection(Direction direction, Room connectedRoom)
        {
            Direction = direction;
            ConnectedRoom = connectedRoom;
        }
    }
}
