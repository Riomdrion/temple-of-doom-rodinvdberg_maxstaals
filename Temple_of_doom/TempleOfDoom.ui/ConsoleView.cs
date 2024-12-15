using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.ui;

public class ConsoleView
{
    public void DisplayRoom(Room room, Player player)
    {
        int consoleWidth = Console.WindowWidth;
        int consoleHeight = Console.WindowHeight;
        int startX = (consoleWidth / 2) - (room.Width / 2);
        int startY = (consoleHeight / 2) - (room.Height / 2);

        for (var y = 0; y < room.Height; y++)
        {
            for (var x = 0; x < room.Width; x++)
            {
                int cursorX = startX + x;
                int cursorY = startY + y;

                Console.SetCursorPosition(cursorX, cursorY);

                if (player.Position.X == x && player.Position.Y == y)
                {
                    Console.Write('@');
                }
                else
                {
                    Console.Write(room.Layout[y, x]); 
                }
            }
        }

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
            ConsoleKey.Q => "quit",
            _ => string.Empty 
        };
    }
}