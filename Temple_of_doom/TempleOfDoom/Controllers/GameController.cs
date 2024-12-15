using TempleOfDoom.Data;
using TempleOfDoom.data.Models.Map;
using TempleOfDoom.ui;

namespace TempleOfDoom.Controllers
{
    public class GameController
    {
        private GameWorld? _gameWorld;
        private ConsoleView _view = new();

        public void StartGame()
        {
            // Initialize components
            _view = new ConsoleView();

                _gameWorld = JsonFileReader.LoadGameWorld("../../../../TempleOfDoom.data/Levels/TempleOfDoom.json");
                _gameWorld.Player.Position = _gameWorld.Player.GetPlayerStartPosition();

            // Start the game loop
            while (!_gameWorld.IsGameOver)
            {
                _view.DisplayRoom(_gameWorld.Player.CurrentRoom, _gameWorld.Player);
                var command = _view.GetPlayerArrowInput();
                ProcessCommand(command);
                Console.WriteLine($"Player currentroom: {_gameWorld.Player.CurrentRoom.Id}");
                _view.DisplayRoom(_gameWorld.Player.CurrentRoom, _gameWorld.Player);
            }

                _view.DisplayGameOver(_gameWorld.Player.HasWon);

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
                _gameWorld.Player.Move(command, _gameWorld.Player.CurrentRoom, _gameWorld.Rooms);
            }
        }
    }
}