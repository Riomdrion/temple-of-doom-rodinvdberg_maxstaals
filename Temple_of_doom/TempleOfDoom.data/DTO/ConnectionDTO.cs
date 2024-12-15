namespace TempleOfDoom.data.DTO;

public class ConnectionDto
{
    public int? NORTH { get; set; }
    public int? SOUTH { get; set; }
    public int? EAST { get; set; }
    public int? WEST { get; set; }
    public List<DoorDto> Doors { get; set; }
}
