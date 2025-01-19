using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.FloorTiles
{
    public abstract class FloorTile(Position position)
    {
        public Position posistion { get; set; } = position;

        public char Symbol { get; set; } = (char)Symbols.CONVEYORBELTNORTH;
        public abstract void Effect(Player player, Room room);

        private char GetSymbolForDirection(Direction direction)
        {
            return direction switch
            {
                Direction.NORTH => (char)Symbols.CONVEYORBELTNORTH,
                Direction.SOUTH => (char)Symbols.CONVEYORBELTSOUTH,
                Direction.EAST => (char)Symbols.CONVEYORBELTEAST,
                Direction.WEST => (char)Symbols.CONVEYORBELTWEST,
                _ => throw new ArgumentException("Invalid conveyor belt direction")
            };
        }
    }
}
