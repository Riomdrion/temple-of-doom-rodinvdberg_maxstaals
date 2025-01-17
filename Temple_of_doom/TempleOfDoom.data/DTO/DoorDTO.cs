using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.DTO;

public class DoorDto
{
    public string type { get; }
    public string? color { get; }
    public int? no_of_stones { get; }
}