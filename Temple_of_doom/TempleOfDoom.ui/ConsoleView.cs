using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.ui;

public class ConsoleView
{
    public void DisplayRoom(Room room, Player player)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;  // Set to white
        Console.WriteLine($"Debug: Player Position -> X: {player.Position.X}, Y: {player.Position.Y}");
        Console.WriteLine($"Room: Width = {room.Width}, Height = {room.Height}");

        // Bereken consolebreedte/-hoogte en startpositie voor centreren
        int consoleWidth = Console.WindowWidth;
        int consoleHeight = Console.WindowHeight;
        int startX = Math.Max((consoleWidth - room.Width * 2) / 2, 0);
        int startY = Math.Max((consoleHeight - room.Height) / 2, 0);

        // Itereer door de kamerhoogte en -breedte
        for (var y = 0; y < room.Height; y++)
        {
            for (var x = 0; x < room.Width; x++)
            {
                // Bereken de positie in de console
                int cursorX = startX + (x * 2);  // Dit creëert extra ruimte door de x-coördinaat te vermenigvuldigen
                int cursorY = startY + y;  // Dit creëert extra ruimte door de y-coördinaat te vermenigvuldigen

                Console.SetCursorPosition(cursorX, cursorY);

                // Teken de randen met `#` (muren)
                if (y == 0 || y == room.Height - 1 || x == 0 || x == room.Width - 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;  // Set to yellow
                    Console.Write("#");
                }
                // Teken de speler als deze op de huidige positie staat
                else if (x == player.Position.X && y == player.Position.Y)
                {
                    Console.Write("x");

                    // Handle item pickup (e.g., Sankara Stone)
                    player.PickUpItem(room);

                    // Check if player steps on a disappearing boobytrap
                    var itemAtPosition = room.Items.FirstOrDefault(i => i.X == x && i.Y == y);
                    if (itemAtPosition != null && itemAtPosition.Type == "disappearing boobytrap")
                    {
                        // Remove the disappearing boobytrap from the room
                        room.Items.Remove(itemAtPosition);
                    }
                }
                // Teken de items op hun positie in de kamer
                else
                {
                    var itemAtPosition = room.Items.FirstOrDefault(i => i.X == x && i.Y == y);
                    if (itemAtPosition != null)
                    {
                        // Item type check
                        Console.Write(itemAtPosition.Type == "key" ? "K" :
                                      itemAtPosition.Type == "sankara stone" ? "S" :
                                      itemAtPosition.Type == "boobytrap" ? "O" :
                                      itemAtPosition.Type == "disappearing boobytrap" ? "@" :
                                      itemAtPosition.Type == "pressure plate" ? "T" : " ");
                    }
                    else
                    {
                        // Vul overige ruimte met spaties
                        Console.Write(" ");
                    }
                }
            }
        }

        // Plaats de cursor naar een veilige plek onderaan
        Console.SetCursorPosition(0, consoleHeight - 1);
        Console.WriteLine($"Player: Lives = {player.Lives}, HasWon = {player.HasWon}");
        Console.WriteLine($"Sankara Stones: {player.GetItemCount()}");  // Show the count of Sankara Stones
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