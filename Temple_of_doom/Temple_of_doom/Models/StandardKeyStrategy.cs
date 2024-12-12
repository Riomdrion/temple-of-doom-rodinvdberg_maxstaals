namespace Temple_of_doom.Models
{
    public class StandardKeyStrategy : IDoorStrategy
    {
        public bool CanOpen(Door door, Player player)
        {
            return door is LockedDoor lockedDoor && player.HasKey(lockedDoor.RequiredKeyColor);
        }

        public void OpenDoor(Door door)
        {
            throw new NotImplementedException("OpenDoor not implemented for StandardKeyStrategy");
        }
    }
}