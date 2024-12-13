namespace TempleOfDoom.data.Models.Items;

public class SankaraStone : Item
{
    public SankaraStone(string itemDtoName, int power = 100)
    {
        Power = power;
    }

    public int Power { get; set; }
}