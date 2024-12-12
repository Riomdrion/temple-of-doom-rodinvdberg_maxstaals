namespace Temple_of_doom.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; }
        public List<Door> Doors { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public char[,] Layout { get; set; }
        
        public Room(int id, string name, int width, int height, List<Item> items, List<Door> doors)
        {
            Id = id;
            Name = name;
            Width = width;
            Height = height;
            Layout = new char[height, width];
            Items = items ?? new List<Item>();
            Doors = doors ?? new List<Door>();
        }
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
                    Layout[player.Position.Y, player.Position.X] = '.';
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
                        return new Position(x, y);
                    }
                }
            }

            throw new Exception("Player start position not defined in room layout.");
        }
    }
}