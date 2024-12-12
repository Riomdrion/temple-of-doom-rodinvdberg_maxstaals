namespace Temple_of_doom.Models
{
    public class PressurePlatePublisher
    {
        public void Notify(PressurePlate plate)
        {
            if (plate.IsActive)
            {
                Console.WriteLine("Pressure plate activated!");
            }
        }
    }
}
