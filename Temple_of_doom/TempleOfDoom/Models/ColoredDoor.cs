
namespace TempleOfDoom.Models
{
    public class ColoredDoor : Door
    {
        public ColoredDoor(int doorDtoId, int doorDtoTargetRoomId)
        {
            throw new NotImplementedException("CollorDoor constructor not implemented");
        }

        public Color Color { get; set; }
        public string Id { get; set; }
        public string KeyColor { get; set; }

        public override bool CanOpen(Player player)
        {
            throw new NotImplementedException("CANOPEN not implemented for ColoredDoor");
        }

        public void Open()
        {
            Console.WriteLine($"The {Color} door is opening...");
        }
    }

}
