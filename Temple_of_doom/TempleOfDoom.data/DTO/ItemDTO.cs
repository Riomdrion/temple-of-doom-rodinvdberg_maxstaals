namespace TempleOfDoom.data.DTO;

public class ItemDto
{
    public string Type { get; set; }
    public int X { get; set; }  // Add X and Y coordinates
    public int Y { get; set; }
    public string? color { get; set; }  // Add KeyColor property
    public int? damage { get; set; }  // Add Damage property
}