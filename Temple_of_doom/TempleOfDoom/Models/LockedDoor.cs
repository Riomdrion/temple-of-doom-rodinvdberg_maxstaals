namespace TempleOfDoom.Models
{
    public class LockedDoor : Door
    {
        public LockedDoor(int doorDtoId, int doorDtoTargetRoomId)
        {
            throw new NotImplementedException("LockedDoor constructor not implemented");
        }

        public string RequiredKeyColor { get; set; }
        public string Id { get; set; }

        public override bool CanOpen(Player player)
        {
            return player.HasKey(RequiredKeyColor);
        }

        public void Open()
        {
            throw new NotImplementedException("Open not implemented for LockedDoor");
        }
    }
}
