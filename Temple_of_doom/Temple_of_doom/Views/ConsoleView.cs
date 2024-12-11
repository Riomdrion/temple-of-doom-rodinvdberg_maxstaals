using Temple_of_doom.Models;

namespace Temple_of_doom.Views
{
    public class ConsoleView
    {
        public void DisplayRoom(Room room)
        {
            new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
