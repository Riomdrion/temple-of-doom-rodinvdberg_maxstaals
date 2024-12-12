namespace Temple_of_doom.Models
{
    public class Room
    {
        private int id;
        private int size;
        private List<Item> items;
        private String Name;
        private readonly List<Door> doors;
        private readonly char[,] layout;

        public Room(string name, List<Item> list, List<Door> doors, char[,] layout)
        {
            this.Name = name;
            this.items = list;
            this.doors = doors;
            this.layout = layout;
        }

        public Room()
        {
            throw new NotImplementedException();
        }

        public List<Connection> Connections { get; } = new();

        public void AddConnection(Connection connection)
        {
            Connections.Add(connection);
        }


        public string GetName()
        {
            return Name;
        }
    }
}