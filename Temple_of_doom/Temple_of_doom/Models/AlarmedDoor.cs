using Temple_of_doom.Models;

namespace Temple_of_doom.Models
{
    public class AlarmedDoor : DoorDecorator
    {
        public AlarmedDoor(Door door) : base(door) { }


        public override void Open()
        {
            throw new NotImplementedException();
        }
    }
}
