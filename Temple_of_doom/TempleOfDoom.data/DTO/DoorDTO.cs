using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.DTO;

public class DoorDto
{
    public int Id { get; }
    public int TargetRoomId { get; }
    public string Type { get; }
    public string? KeyColor { get; }
    public Direction Direction { get; }
    public int RequiredStones { get; }
}