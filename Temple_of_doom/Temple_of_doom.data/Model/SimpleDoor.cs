using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temple_of_doom.Model;

namespace Temple_of_doom.data.Model
{
    public class SimpleDoor : Door
    {
        public override void Open()
        {
            Console.WriteLine("Simple door opens.");
        }
    }
}
