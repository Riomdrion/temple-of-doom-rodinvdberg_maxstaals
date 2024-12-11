

namespace Temple_of_doom.Models
{
    public abstract class DoorDecorator : Door
    {
        protected Door door;

        protected DoorDecorator(Door door)
        {
            this.door = door;
        }
    }
}
