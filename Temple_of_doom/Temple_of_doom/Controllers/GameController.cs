
using Temple_of_doom.Data;
using Temple_of_doom.Models;
using Temple_of_doom.Views;

namespace Temple_of_doom.Controllers
{
        public class GameController
    {
        private readonly GameWorld gameWorld = JsonFileReader.LoadGameWorld("C:\\Users\\rodin\\OneDrive\\Documenten\\GitHub\\avans-code\\deelopdracht-1-24-25-temple-of-doom-rodinvdberg_maxstaals-1\\Temple_of_doom\\Temple_of_doom\\TempleOfDoom.json");
        private readonly ConsoleView view = new();

        public void StartGame()
        {
            // Assign player start Position
            gameWorld.Player.Position = gameWorld.CurrentRoom.GetPlayerStartPosition();

            // Start the game loop
            while (!gameWorld.IsGameOver)
            {
                view.DisplayRoom(gameWorld.CurrentRoom, gameWorld.Player);
                var command = view.GetPlayerInput();
                ProcessCommand(command);
            }

            view.DisplayGameOver(gameWorld.Player.HasWon);
        }

        private void ProcessCommand(string command)
        {
            // Handle player movement and interactions
            if (command == "up" || command == "down" || command == "left" || command == "right")
            {
                gameWorld.MovePlayer(command);
            }
            else
            {
                view.DisplayInvalidCommand();
            }
        }
    }
}

