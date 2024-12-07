using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temple_of_doom.Model
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
