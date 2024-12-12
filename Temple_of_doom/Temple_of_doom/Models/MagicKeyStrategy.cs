namespace Temple_of_doom.Models
{
    public class MagicKeyStrategy : IDoorStrategy
    {
        public bool CanOpen(Door door, Player player)
        {
            return true; // Magic keys can open any door
        }

        public void OpenDoor(Door door)
        {
            throw new NotImplementedException();
        }
    }
}
