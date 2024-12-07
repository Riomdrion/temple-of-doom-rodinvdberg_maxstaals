using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temple_of_doom.Model;

namespace Temple_of_doom.data.Model
{
    public abstract class DoorDecorator : Door
    {
        protected Door door;

        protected DoorDecorator(Door door)
        {
            this.door = door;
        }

        public override void Open()
        {
            door.Open();
        }
    }
}
