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
        public Door CreateDoor(string type)
        {
            return type switch
            {
                "ColoredDoor" => new ColoredDoor(),
                "ToggleDoor" => new ToggleDoor(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
