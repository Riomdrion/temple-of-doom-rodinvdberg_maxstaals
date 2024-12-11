
using Temple_of_doom.Data;
using Temple_of_doom.Models;
using Temple_of_doom.Views;

namespace Temple_of_doom.Controllers
{
    public class GameController
    {
        private GameWorld _gameWorld;
        private ConsoleView _view;

        public GameController()
        {
            _gameWorld = new GameWorld();
            _view = new ConsoleView();
        }

        public void StartGame()
        {
            // Initialize components
            _view = new ConsoleView();
            _gameWorld = JsonFileReader.LoadGameWorld("Data/TempleOfDoom.json");

            // Start the game loop
            while (true)
            {
                _view.DisplayRoom(_gameWorld.CurrentRoom);
                var command = _view.GetPlayerInput();
                ProcessCommand(command);
            }
        }

        private void ProcessCommand(string command)
        {
            // Handle player movement and interactions
            if (command == "up" || command == "down" || command == "left" || command == "right")
            {
                _gameWorld.MovePlayer(command);
            }
            else
            {
                _view.DisplayInvalidCommand();
            }
        }
    }
}

