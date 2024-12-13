using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.ui;

public class ConsoleView
{
    public void DisplayRoom(Room room, Player player)
    {
        Console.Clear();
        for (var y = 0; y < room.Layout.GetLength(0); y++)
        {
            for (var x = 0; x < room.Layout.GetLength(1); x++)
            {
                // Controleer of het een randpositie is
                if (y == 0 || y == room.Layout.GetLength(0) - 1 || x == 0 || x == room.Layout.GetLength(1) - 1)
                {
                    Console.Write("# "); // Rand
                }
                else if (player.Position.X == x && player.Position.Y == y)
                {
                    Console.Write("@ "); // Speler
                }
                else
                {
                    Console.Write("  "); // Lege ruimte
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
        var keyInfo = Console.ReadKey(true);

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