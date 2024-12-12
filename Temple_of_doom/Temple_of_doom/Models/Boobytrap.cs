namespace Temple_of_doom.Models
{
    public class Boobytrap :Item
    {
        public int Damage { get; set; }
        public bool Disappearing { get; set; }
        public void Trigger(Player player)
        {
            player.Lives -= Damage;
        }
    }

}