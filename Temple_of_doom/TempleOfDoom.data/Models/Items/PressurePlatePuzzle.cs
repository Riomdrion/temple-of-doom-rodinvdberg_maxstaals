namespace TempleOfDoom.data.Models.Items;

public class PressurePlatePuzzle
{
    public List<PressurePlate> Plates { get; set; } = new();

    public bool IsSolved => Plates.All(p => p.IsActive);
}