using Temple_of_doom.Models;

namespace Temple_of_doom.Views
{
    public class ConsoleView
    {
        public void DisplayRoom(Room room, Player player)
        {
            Console.Clear();
            Console.WriteLine($"You are in {room.Name}");

            for (int y = 0; y < room.Layout.GetLength(0); y++)
            {
                for (int x = 0; x < room.Layout.GetLength(1); x++)
                {
                    if (player.Position.X == x && player.Position.Y == y)
                    {
                        Console.Write('X'); // Display player position
                    }
                    else
                    {
                        Console.Write(room.Layout[y, x]);
                    }
                }
                Console.WriteLine();
            }
        }

        public ConsoleKey GetPlayerKeyInput()
        {
            Console.WriteLine("Use arrow keys to move.");
            return Console.ReadKey(true).Key;
        }

        public void DisplayInvalidCommand()
        {
            Console.WriteLine("Invalid command. Please try again.");
        }

        public void DisplayGameOver(bool hasWon)
        {
            Console.WriteLine(hasWon ? "You have won!" : "Game over!");
        }

        public string GetPlayerInput()
        {
            throw new NotImplementedException("GetPlayerInput not implemented");
        }
    }
}
