
using Temple_of_doom.Models;

namespace Temple_of_doom.Factories
{
    public abstract class DoorFactory
    {
        public Door CreateDoor(string type, Color color)
        {
            return type switch
            {
                "SimpleDoor" => new SimpleDoor(),
                "ColoredDoor" => new Models.ColoredDoor { Color = Color.RED },
                "ToggleDoor" => new ToggleDoor(),
                _ => throw new ArgumentException("Unknown door type")
            };
        }
    }
}
