using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Items;

public class Boobytrap : Item
{
    public Boobytrap(int value)
    {
        Damage = value;
    }

    public int Damage { get; set; }

    public void Trigger(Player player)
    {
        player.Lives -= Damage;
    }
}