namespace Temple_of_doom.Models
{
    public class SimpleDoor : Door
    {
        public bool CanOpen(Player player)
        {
            return true;
        }

        public override void Open()
        {
            throw new NotImplementedException();
        }
    }
}

