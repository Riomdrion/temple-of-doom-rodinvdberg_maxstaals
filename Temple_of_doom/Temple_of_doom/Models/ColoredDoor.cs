
namespace Temple_of_doom.Models
{
    public class ColoredDoor : Door
    {
        public Color Color { get; set; }

        public override void Open()
        {
            Console.WriteLine($"The {Color} door is opening...");
        }
    }

}
