namespace Temple_of_doom.Models
{
    public class MagicKeyStrategy : IDoorStrategy
    {
        public void OpenDoor(Door door)
        {
            Console.WriteLine("Door opened magically!");
        }
    }
}
