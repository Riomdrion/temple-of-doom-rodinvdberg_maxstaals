using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temple_of_doom.Model
{
    public class ColoredDoor : Door
    {
        public string Color { get; set; } = "Red";

        public ColoredDoor(string type) : base(type)
        {
        }
    }

}
