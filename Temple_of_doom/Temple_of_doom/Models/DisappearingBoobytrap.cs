namespace Temple_of_doom.Models;

public class DisappearingBoobytrap : Item
{
    public int Damage { get; }

    public DisappearingBoobytrap(string name, int damage) : base()
    {
        Damage = damage;
    }

    public void Activate(Player player)
    {
        player.Lives -= Damage;
        Console.WriteLine($"{Name} activated! {Damage} damage dealt to the player. Remaining lives: {player.Lives}");
    }
}