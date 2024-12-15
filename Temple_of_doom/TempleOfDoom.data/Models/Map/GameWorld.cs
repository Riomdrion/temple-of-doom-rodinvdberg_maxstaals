using Newtonsoft.Json;

namespace TempleOfDoom.data.Models.Map;

public class GameWorld(Player player, List<Room> rooms)
{
    public Player Player { get; set; } = player;
    public List<Room> Rooms { get; set; } = rooms;
    public bool IsGameOver => Player?.Lives <= 0 || Player?.HasWon == true;
}