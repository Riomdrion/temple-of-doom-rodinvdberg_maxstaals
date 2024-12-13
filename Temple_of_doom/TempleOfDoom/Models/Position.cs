namespace TempleOfDoom.Models;

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }
    public Position(int startX, int startY)
    {
        X = startX;
        Y = startY;
    }


}