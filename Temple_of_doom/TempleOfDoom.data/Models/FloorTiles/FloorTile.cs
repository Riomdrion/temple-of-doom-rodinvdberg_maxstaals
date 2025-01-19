using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.FloorTiles
{
    public abstract class FloorTile(Position position)
    {
        private Position posistion { get; set; } = position;

        public abstract void Effect(Player player, Room room);
    }
}
