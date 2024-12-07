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
        public Color Color { get; set; }

        public override void Open()
        {
            Console.WriteLine($"The {Color} door is opening...");
        }
    }

}
