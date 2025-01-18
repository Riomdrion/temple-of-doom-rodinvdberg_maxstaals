using TempleOfDoom.data.Enums;

namespace TempleOfDoom.data.Models.Map;

public class Ladder
{
    public Position UpperPosition { get; }
    public Position LowerPosition { get; }
    public int UpperRoomId { get; }
    public int LowerRoomId { get; }
    public char Symbol { get; } = (char)Symbols.LADDER;
    
    

    public Ladder(int upperRoomId, Position upperPosition, int lowerRoomId, Position lowerPosition)
    {
        UpperRoomId = upperRoomId;
        UpperPosition = upperPosition;
        LowerRoomId = lowerRoomId;
        LowerPosition = lowerPosition;
    }

    public bool IsAtPosition(Position position)
    {
        return position.Equals(UpperPosition) || position.Equals(LowerPosition);
    }
}