using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temple_of_doom.Model;

namespace Temple_of_doom.data.Model
{
    public class LockedDoor : DoorDecorator
    {
        public LockedDoor(Door door) : base(door) { }

        public override void Open()
        {
            Console.WriteLine("Unlocking the door...");
            base.Open();
        }
    }
}
