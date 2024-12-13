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
        Console.Clear();  // Dit wist de hele console, wat misschien niet nodig is

        Console.WriteLine($"You are in {room.Name}");
        Console.WriteLine($"Debug: Player Position -> X: {player.Position.X}, Y: {player.Position.Y}");

        // Controleer of de spelerpositie binnen de grenzen van de kamer ligt
        if (player.Position.X < 0 || player.Position.X >= room.Layout.GetLength(1) ||
            player.Position.Y < 0 || player.Position.Y >= room.Layout.GetLength(0))
        {
            Console.WriteLine("Error: Player position is out of bounds!");
            return;
        }

        // Verkrijg de breedte en hoogte van de console
        int consoleWidth = Console.WindowWidth;
        int consoleHeight = Console.WindowHeight;

        // Bereken het midden van de console
        int centerX = consoleWidth / 2;
        int centerY = consoleHeight / 2;

        // Verschuif de startpositie van de kamer 1/4 van de breedte naar links
        int offsetX = consoleWidth / 4;
        int startX = centerX - room.Layout.GetLength(1) / 2 - offsetX; // Verplaats naar links
        int startY = centerY - room.Layout.GetLength(0) / 2;

        // Loop door het kamerrooster en teken de lay-out
        for (var y = 0; y < room.Layout.GetLength(0); y++) // Y-as (rijen)
        {
            // Verplaats de cursor naar de juiste y-positie in de console
            Console.SetCursorPosition(startX, startY + y);

            for (var x = 0; x < room.Layout.GetLength(1); x++) // X-as (kolommen)
            {
                if (x == player.Position.X && y == player.Position.Y)
                {
                    // Zet de cursor naar de nieuwe spelerpositie en teken 'X'
                    Console.SetCursorPosition(startX + x, startY + y);
                    Console.Write('@'); // De speler wordt weergegeven als '@'
                }
                else
                {
                    Console.Write("  "); // Lege ruimte
                }
            }
            Console.WriteLine();
                {
                    Console.Write(room.Layout[y, x]); // Weergeef de tegelinhoud
                }
            }
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