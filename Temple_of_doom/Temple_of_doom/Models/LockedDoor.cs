namespace Temple_of_doom.Models
{
    public class LockedDoor : DoorDecorator
    {
        public LockedDoor(Door door) : base(door) { }

        public override void Open()
        {
            Console.WriteLine("Unlocking the door...");
        }
    }
}
