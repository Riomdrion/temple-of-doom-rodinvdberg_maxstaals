namespace Temple_of_doom.Models
{
    public class LockedDoor : Door
    {
        public LockedDoor(int doorDtoId, int doorDtoTargetRoomId)
        {
            throw new NotImplementedException();
        }

        public string RequiredKeyColor { get; set; }
        public string Id { get; set; }

        public override bool CanOpen(Player player)
        {
            return player.HasKey(RequiredKeyColor);
        }

        public void Open()
        {
            throw new NotImplementedException();
        }
    }
}
