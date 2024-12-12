namespace Temple_of_doom.Models
{
    public class SankaraStone : Item
    {
        public int Power { get; set; }

        public SankaraStone(string itemDtoName, int power = 100)
        {
            Power = power;
        }
    }
}