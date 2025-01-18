namespace TempleOfDoom.data.DTO;

public class ConnectionDto
{
    public int? north { get; set; }
    public int? south { get; set; }
    public int? east { get; set; }
    public int? west { get; set; }
    public int? upper { get; set; }
    public int? lower { get; set; }
    public List<DoorDto> Doors { get; set; }
    public List<LadderDto> Ladders { get; set; } 
}
