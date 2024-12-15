using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.ui;

public class ConsoleView
{
    public void DisplayRoom(Room room, Player player)
    {
        Console.Clear();
        Console.SetCursorPosition(0, Console.WindowHeight - 2);
        Console.WriteLine($"Player: ({player.Position.X}, {player.Position.Y})");
        Console.WriteLine("Aantal levens: " + player.Lives);
        Console.WriteLine("Aantal sankara stones: "+ player.GetItemCount());

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

                // Check if the player is at this position
                if (player.Position.X == x && player.Position.Y == y)
                {
                    Console.Write('@');
                }
                else
                {
                    // Check if there's an item at this position
                    var item = room.Items.FirstOrDefault(i => i.X == x && i.Y == y);
                    if (item != null)
                    {
                        Console.Write(item.Type switch
                        {
                            "key" => 'K',
                            "sankara stone" => 'S',
                            "boobytrap" => 'O',
                            "disappearing boobytrap" => '@',
                            "pressure plate" => 'T',
                            _ => '?', // Default character for unknown items
                        });
                    }
                    else
                    {
                        // Default to the room layout if no item is present
                        Console.Write(room.Layout[y, x]);
                    }
                }
            }
        }

        Console.SetCursorPosition(0, consoleHeight-1); 
    }



    public static void DisplayGameOver(bool hasWon)
    {
        Console.WriteLine();
        Console.WriteLine(hasWon ? "You have won! " : "Game over! ");
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