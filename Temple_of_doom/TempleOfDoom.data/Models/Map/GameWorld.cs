namespace TempleOfDoom.data.Models.Map;

public class GameWorld
{
    public Player Player { get; set; }
    public List<Room> Rooms { get; set; }
    public Room CurrentRoom { get; set; }
    public bool IsGameOver => Player?.Lives <= 0 || Player?.HasWon == true;

    
}