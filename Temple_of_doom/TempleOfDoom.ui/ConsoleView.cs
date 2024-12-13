using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.ui;

public class ConsoleView
{
    public void DisplayRoom(Room room, Player player)
    {
        Console.Clear();
        Console.WriteLine($"Debug: Room Dimensions -> Width: {room.Width+1}, Height: {room.Height+1}");
        Console.WriteLine($"Debug: Player Position -> X: {player.Position.X}, Y: {player.Position.Y}");

        // Bereken consolebreedte/-hoogte en startpositie voor centreren
        int consoleWidth = Console.WindowWidth;
        int consoleHeight = Console.WindowHeight;
        int startX = (consoleWidth / 2) - (room.Width / 2);
        int startY = (consoleHeight / 2) - (room.Height / 2);

        // Itereer door de kamerhoogte en -breedte
        for (var y = 0; y < room.Height; y++)
        {
            for (var x = 0; x < room.Width; x++)
            {
                // Bereken de positie in de console
                int cursorX = startX + x;
                int cursorY = startY + y;

                Console.SetCursorPosition(cursorX, cursorY);

                // Teken de randen met `#`
                if (y == 0 || y == room.Height - 1 || x == 0 || x == room.Width - 1)
                {
                    Console.Write("#");
                }
                // Teken de speler als deze op de huidige positie staat
                else if (x == player.Position.X && y == player.Position.Y)
                {
                    Console.Write("@");
                }
                // Vul overige ruimte met spaties
                else
                {
                    Console.Write(" ");
                }
            }
        }

        // Plaats de cursor naar een veilige plek onderaan
        Console.SetCursorPosition(0, consoleHeight - 1);
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