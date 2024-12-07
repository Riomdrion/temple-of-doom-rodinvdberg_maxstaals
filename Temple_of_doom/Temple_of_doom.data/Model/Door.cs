using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temple_of_doom.data.Model;

namespace Temple_of_doom.Model
{
    public abstract class Door : UIObserver
    {

        public Door()
        {
        }

        public void Update()
        {
            Console.WriteLine("Door is opening in response to PressurePlate.");
        }

        public abstract void Open();
    }
}
