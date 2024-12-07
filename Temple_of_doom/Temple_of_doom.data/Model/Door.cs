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

        public void Update()
        {
            Console.WriteLine("Door has been notified and will open.");
            Open();
        }

        public virtual void Open()
        {
            Console.WriteLine("The door is opening...");
        }
    }
}
