using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.DTO;

public class DoorDto
{
    public int Id { get; set; }
    public int TargetRoomId { get; set; }
    public string Type { get; set; }
    public string? KeyColor { get; set; }
    public Direction Direction { get; set; }
    public int RequiredStones { get; set; }
}