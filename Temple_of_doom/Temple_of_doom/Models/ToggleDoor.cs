namespace Temple_of_doom.Models
{
    public class ToggleDoor : Door
    {
        public bool CanOpen(Player player)
        {
            return true;
        }

        public override void Open()
        {
            throw new NotImplementedException();
        }

        public string Id { get; set; }
    }
}
