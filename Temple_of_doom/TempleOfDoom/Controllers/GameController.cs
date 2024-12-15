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

            ValidateFactories(); // Validate door and connection logic

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
                Console.WriteLine($"Processing command: {command}");
                var previousRoom = _gameWorld.Player.CurrentRoom;
                _gameWorld.Player.Move(command, _gameWorld.Player.CurrentRoom, _gameWorld.Rooms);
                if (previousRoom != _gameWorld.Player.CurrentRoom)
                {
                    Console.WriteLine($"Player moved to Room ID: {_gameWorld.Player.CurrentRoom.Id}");
                }
            }
        }

        private void ValidateFactories()
        {
            foreach (var room in _gameWorld.Rooms)
            {
                if (room.Doors.Count == 0)
                {
                    Console.WriteLine($"Warning: Room ID={room.Id} has no doors.");
                }

                foreach (var door in room.Doors)
                {
                    if (door.TargetRoomId <= 0)
                    {
                        Console.WriteLine($"Warning: Invalid TargetRoomId for Door ID={door.Id} in Room ID={room.Id}.");
                    }
                }
            }
        }
    }
}
