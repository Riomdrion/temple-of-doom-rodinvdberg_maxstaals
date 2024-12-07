using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temple_of_doom.Model
{
    public class Room
    {
        private int id;
        private int size;
        private List<Item> items;
    }

    public List<Connection> Connections { get; } = new();

        public void AddConnection(Connection connection)
        {
            Connections.Add(connection);
        }
    }

}
