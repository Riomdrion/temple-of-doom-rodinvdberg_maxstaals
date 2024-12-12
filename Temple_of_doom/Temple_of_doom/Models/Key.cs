namespace Temple_of_doom.Models
{
    public class Key : Item
    {
        public string KeyColor { get; set; }

        public Key(string itemDtoName) : base()
        {
            KeyColor = itemDtoName;
        }
    }
}