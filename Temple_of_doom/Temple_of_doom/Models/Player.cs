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

        public void SetStrategy(IDoorStrategy strategy)
        {
            this.strategy = strategy;
        }

        public void OpenDoor(Door door)
        {
            strategy.OpenDoor(door);
        }

    }
}
