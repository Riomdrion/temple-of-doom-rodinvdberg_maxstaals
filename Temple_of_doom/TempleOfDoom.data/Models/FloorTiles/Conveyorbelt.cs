using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.FloorTiles
{
    public class Conveyorbelt(Position position) : FloorTile(position)
    {
        public override void Effect(Player player, Room room)
        {
        }
    }
}
