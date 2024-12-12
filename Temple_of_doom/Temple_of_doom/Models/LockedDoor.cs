namespace Temple_of_doom.Models
{
    public class LockedDoor : Door
    {
        public string RequiredKeyColor { get; set; }
        public string Id { get; set; }

        public bool CanOpen(Player player)
        {
            return player.HasKey(RequiredKeyColor);
        }

        public override void Open()
        {
            throw new NotImplementedException();
        }
    }
}
