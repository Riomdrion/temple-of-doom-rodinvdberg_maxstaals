namespace Temple_of_doom.Models
{
    public class Player
    {
        public int Lives { get; set; }
        public bool HasWon { get; set; }
        public Position Position { get; set; }
        public Inventory Inventory { get; set; }
        public string StartingRoomId { get; set; }
        public int StartX { get; }
        public int StartY { get; }


        public Player()
        {
            Inventory = new Inventory();
            Position = new Position();
        }

        public bool HasKey(string keyColor)
        {
            return Inventory.HasItem(keyColor);
        }
    }
}
