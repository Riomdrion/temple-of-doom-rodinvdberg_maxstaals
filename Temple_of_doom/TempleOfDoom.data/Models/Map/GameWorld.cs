using Newtonsoft.Json;

namespace TempleOfDoom.data.Models.Map;

public class GameWorld
{
    [JsonProperty("player")]
    public Player Player { get; set; }
    [JsonProperty("rooms")]
    public List<Room> Rooms { get; set; }
    public Room CurrentRoom { get; set; }
    public bool IsGameOver => Player?.Lives <= 0 || Player?.HasWon == true;

    
}