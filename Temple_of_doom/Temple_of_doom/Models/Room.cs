namespace Temple_of_doom.Models
{
    public class Room
    {
        // Constructor with parameters
        public Room(string name, List<Item> items, List<Door> doors, char[,] layout)
        {
            Name = name;
            Items = items ?? new List<Item>();
            Doors = doors ?? new List<Door>();
            Layout = layout ?? throw new ArgumentNullException(nameof(layout), "Room layout cannot be null.");
        }

        // Default constructor
        public Room()
        {
            Name = "Unnamed Room";
            Items = new List<Item>();
            Doors = new List<Door>();
            Layout = new char[5, 5] // Default layout
            {
                { '#', '#', '#', '#', '#' },
                { '#', 'X', '.', '.', '#' },
                { '#', '.', '#', 'S', '#' },
                { '#', 'K', '.', '.', '#' },
                { '#', '#', '#', '#', '#' }
            };
        }

        public string Name { get; set; }
        public List<Item> Items { get; set; }
        public List<Door> Doors { get; set; }
        public char[,] Layout { get; set; } // 2D layout representing walls, items, etc.

        public bool IsPositionWalkable(Position position)
        {
            if (Layout == null)
                throw new NullReferenceException("Room layout is not initialized.");

            if (position.Y < 0 || position.Y >= Layout.GetLength(0) ||
                position.X < 0 || position.X >= Layout.GetLength(1))
            {
                return false;
            }

            return Layout[position.Y, position.X] != '#';
        }

        public void HandlePlayerInteraction(Player player)
        {
            if (Layout == null)
                throw new NullReferenceException("Room layout is not initialized.");

            char currentTile = Layout[player.Position.Y, player.Position.X];

            switch (currentTile)
            {
                case 'S':
                    player.Inventory.AddItem("Sankara Stone");
                    Layout[player.Position.Y, player.Position.X] = '.'; // Remove the item
                    Console.WriteLine("You picked up a Sankara Stone!");
                    break;
                case 'K':
                    player.Inventory.AddItem("Key");
                    Layout[player.Position.Y, player.Position.X] = '.';
                    Console.WriteLine("You picked up a Key!");
                    break;
            }
        }

        public Position GetPlayerStartPosition()
        {
            if (Layout == null)
                throw new NullReferenceException("Room layout is not initialized.");

            for (int y = 0; y < Layout.GetLength(0); y++)
            {
                for (int x = 0; x < Layout.GetLength(1); x++)
                {
                    if (Layout[y, x] == 'X')
                    {
                        return new Position { X = x, Y = y };
                    }
                }
            }
            throw new Exception("Player start position not defined in room layout.");
        }
    }
}
