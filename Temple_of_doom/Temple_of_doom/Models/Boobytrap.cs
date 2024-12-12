namespace Temple_of_doom.Models
{
    public class Boobytrap
    {
        public int Damage { get; set; }
        public void Trigger(Player player)
        {
            player.Lives -= Damage;
        }
    }

}