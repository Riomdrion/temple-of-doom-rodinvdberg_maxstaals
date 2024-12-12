namespace Temple_of_doom.Models
{
    public class ToggleDoor : Door
    {
        public override bool CanOpen(Player player)
        {
            return true;
        }

        public void Open()
        {
            throw new NotImplementedException("Open not implemented for ToggleDoor");
        }

        public string Id { get; set; }
    }
}
