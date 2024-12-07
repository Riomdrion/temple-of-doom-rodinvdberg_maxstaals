using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temple_of_doom.Model;

namespace Temple_of_doom.data.Model
{
    public class MagicKeyStrategy : IDoorStrategy
    {
        public void OpenDoor(Door door)
        {
            Console.WriteLine("Door opened magically!");
        }
    }
}
