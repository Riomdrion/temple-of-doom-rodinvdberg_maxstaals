namespace Temple_of_doom.Models
{
    public class SimpleDoor : Door
    {
        public override bool CanOpen(Player player)
        {
            return true;
        }

        public void Open()
        {
            throw new NotImplementedException();
        }
    }
}

