namespace Temple_of_doom.Models
{
    public class Player
    {
        private int startRoomID;
        private int[] startCoordinates;
        private int lives;
        private List<Item> backpack;
        private IDoorStrategy strategy;

        public Player()
        {
        }

        public int Lives { get; set; }

        public void SetStrategy(IDoorStrategy strategy)
        {
            this.strategy = strategy;
        }

        public void OpenDoor(Door door)
        {
            strategy.OpenDoor(door);
        }

        public bool HasKey(string requiredKeyColor)
        {
            throw new NotImplementedException();
        }
    }
}
