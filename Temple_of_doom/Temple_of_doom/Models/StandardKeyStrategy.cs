namespace Temple_of_doom.Models
{
    public class StandardKeyStrategy : IDoorStrategy
    {
        public void OpenDoor(Door door)
        {
            Console.WriteLine("Door opened with a standard key.");
        }
    }
}
