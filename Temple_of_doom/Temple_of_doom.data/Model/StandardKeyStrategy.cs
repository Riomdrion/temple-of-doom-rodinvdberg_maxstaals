using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temple_of_doom.Model;

namespace Temple_of_doom.data.Model
{
    public class StandardKeyStrategy : IDoorStrategy
    {
        public void OpenDoor(Door door)
        {
            Console.WriteLine("Door opened with a standard key.");
        }
    }
}
