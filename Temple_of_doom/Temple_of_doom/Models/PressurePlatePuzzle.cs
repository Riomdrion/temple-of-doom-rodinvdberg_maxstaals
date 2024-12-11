namespace Temple_of_doom.Models
{
    public class PressurePlatePuzzle
    {
        public List<PressurePlate> Plates { get; } = new();
        public List<Door> Doors { get; } = new();

        public void AddPlate(PressurePlate plate) => Plates.Add(plate);
        public void AddDoor(Door door) => Doors.Add(door);

        public void Solve()
        {
            Console.WriteLine("Solving the puzzle...");
            foreach (var plate in Plates)
            {
                plate.Notify();
            }

            foreach (var door in Doors)
            {
                Console.WriteLine("Checking if door opened...");
            }
        }
    }

}
