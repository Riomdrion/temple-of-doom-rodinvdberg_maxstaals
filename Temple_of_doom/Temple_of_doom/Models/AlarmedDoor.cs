using Temple_of_doom.Models;

namespace Temple_of_doom.Models
{
    public class AlarmedDoor : Door
    {
        public bool CanOpen(Player player)
        {
            Console.WriteLine("Alarm triggered!");
            return false;
        }

        public override void Open()
        {
            throw new NotImplementedException();
        }

        public string Id { get; set; }
    }
}
