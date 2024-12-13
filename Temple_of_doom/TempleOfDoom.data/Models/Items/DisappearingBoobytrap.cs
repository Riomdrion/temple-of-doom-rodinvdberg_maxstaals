using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Items;

public class DisappearingBoobytrap : Item
{
    public DisappearingBoobytrap(string name, int damage)
    {
        Damage = damage;
    }

    public int Damage { get; }

    public void Activate(Player player)
    {
        player.Lives -= Damage;
        Console.WriteLine($"{Name} activated! {Damage} damage dealt to the player. Remaining lives: {player.Lives}");
    }
}