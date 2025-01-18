using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.FloorTiles;

public class IceTile(Position position) : FloorTile(position)
{
    public void HandleIceFloorTile(string command, Room currentRoom, dynamic player)
    {
        while (currentRoom.GetFloorTileAt(player.Position) is FloorTile iceTile &&
               iceTile.Symbol == (char)Symbols.ICEFLOORTILE)
        {
            var newPosition = new Position
            (
                command switch
                {
                    "up" => player.Position.X,
                    "down" => player.Position.X,
                    "left" => player.Position.X - 1,
                    "right" => player.Position.X + 1,
                    _ => player.Position.X
                },
                command switch
                {
                    "up" => player.Position.Y - 1,
                    "down" => player.Position.Y + 1,
                    "left" => player.Position.Y,
                    "right" => player.Position.Y,
                    _ => player.Position.Y
                }
            );

            if (!currentRoom.IsPositionWalkable(newPosition))
            {
                break;
            }

            player.Position = newPosition;
        }
    }
}