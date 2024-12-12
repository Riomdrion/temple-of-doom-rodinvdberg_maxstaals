namespace Temple_of_doom.Models
{
    public class SimpleDoor : Door
    {
        public SimpleDoor(int doorDtoId, int doorDtoTargetRoomId)
        {
            throw new NotImplementedException("SimpleDoor constructor not implemented");
        }

        public override bool CanOpen(Player player)
        {
            return true;
        }

        public void Open()
        {
            throw new NotImplementedException("Open not implemented for SimpleDoor");
        }
    }
}

