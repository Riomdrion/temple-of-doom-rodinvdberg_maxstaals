namespace Temple_of_doom.Models
{
    public class Room
    {
        private int id;
        private int size;
        private List<Item> items;
        private String Name;

        public List<Connection> Connections { get; } = new();

        public void AddConnection(Connection connection)
        {
            Connections.Add(connection);
        }


        public string getName()
        {
            return Name;
        }
    }
}