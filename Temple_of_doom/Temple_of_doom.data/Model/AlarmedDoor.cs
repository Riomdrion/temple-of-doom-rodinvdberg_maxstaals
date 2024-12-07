using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temple_of_doom.Model;

namespace Temple_of_doom.data.Model
{
    public class AlarmedDoor : DoorDecorator
    {
        public AlarmedDoor(Door door) : base(door) { }

        public override void Open()
        {
            Console.WriteLine("Alarm triggered!");
            base.Open();
        }
    }
}
