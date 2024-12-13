using TempleOfDoom.Data;
using TempleOfDoom.data.Models.Map;
using TempleOfDoom.ui;

namespace TempleOfDoom.Controllers
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
            try
            {
                _gameWorld = JsonFileReader.LoadGameWorld("../../../../TempleOfDoom.data/Levels/TempleOfDoom.json");
                _gameWorld.Player.Position = _gameWorld.Player.GetPlayerStartPosition();

                // Start the game loop
                while (!_gameWorld.IsGameOver)
                {
                    _view.DisplayRoom(_gameWorld.CurrentRoom, _gameWorld.Player);
                    var command = _view.GetPlayerArrowInput();
                    ProcessCommand(command);
                }

                _view.DisplayGameOver(_gameWorld.Player.HasWon);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Exiting game.");
            }
        }

        private void ProcessCommand(string command)
        {
            if (string.IsNullOrEmpty(command)) return;

            if (command == "quit")
            {
                Console.WriteLine("Exiting game.");
                Environment.Exit(0);
            }
            else
            {
                _gameWorld.Player.Move(command, _gameWorld.CurrentRoom);
            }
        }
    }
}