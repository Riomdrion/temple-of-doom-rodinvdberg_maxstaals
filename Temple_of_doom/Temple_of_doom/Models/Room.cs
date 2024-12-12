namespace Temple_of_doom.Models
{
    public class Room
    {
        public Room(string name, List<Item> items, List<Door> doors, char[,] layout)
        {
            throw new NotImplementedException();
        }

        public string Name { get; set; }
        public List<Item> Items { get; set; }
        public List<Door> Doors { get; set; }
        public char[,] Layout { get; set; } 

        public bool IsPositionWalkable(Position position)
        {
            if (position.Y < 0 || position.Y >= Layout.GetLength(0) ||
                position.X < 0 || position.X >= Layout.GetLength(1))
            {
                return false;
            }

            return Layout[position.Y, position.X] != '#';
        }

        public void HandlePlayerInteraction(Player player)
        {
            char currentTile = Layout[player.Position.Y, player.Position.X];

            switch (currentTile)
            {
                case 'S':
                    player.Inventory.AddItem("Sankara Stone");
                    Layout[player.Position.Y, player.Position.X] = '.'; // Remove the item
                    break;
                case 'K':
                    player.Inventory.AddItem("Key");
                    Layout[player.Position.Y, player.Position.X] = '.';
                    break;
                case 'O':
                    player.Lives -= 1;
                    break;
                case '@':
                    player.Lives -= 1;
                    Layout[player.Position.Y, player.Position.X] = '.'; // Remove trap
                    break;
            }
        }

        public Position GetPlayerStartPosition()
        {
            throw new NotImplementedException();
        }
    }
}