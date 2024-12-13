using TempleOfDoom.Models;

namespace TempleOfDoom.Views
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
        public void DisplayGameOver(bool hasWon)
        {
            Console.WriteLine(hasWon ? "You have won!" : "Game over!");
        }

        public string GetPlayerArrowInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            return keyInfo.Key switch
            {
                ConsoleKey.UpArrow => "up",
                ConsoleKey.DownArrow => "down",
                ConsoleKey.LeftArrow => "left",
                ConsoleKey.RightArrow => "right",
                ConsoleKey.Q => "quit", // Optioneel: sluit het spel
                _ => string.Empty // Onbekende invoer
            };
        }
    }
}