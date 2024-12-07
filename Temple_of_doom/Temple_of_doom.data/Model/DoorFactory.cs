using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temple_of_doom.Model;

namespace Temple_of_doom.data.Model
{
    public abstract class DoorFactory
    {
        public Door CreateDoor(string type, Color color)
        {
            return type switch
            {
                "SimpleDoor" => new SimpleDoor(),
                "ColoredDoor" => new ColoredDoor { Color = Color.RED },
                "ToggleDoor" => new ToggleDoor(),
                _ => throw new ArgumentException("Unknown door type")
            };
        }
    }
}
