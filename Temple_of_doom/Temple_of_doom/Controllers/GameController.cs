using System;
using Temple_of_doom.Models;
using Temple_of_doom.Views;
using Temple_of_doom.Data;

namespace Temple_of_doom.Controllers
{
    public class GameController
    {
        private GameWorld _gameWorld;
        private ConsoleView _view;

        public void StartGame()
        {
            // Initialize components
            _view = new ConsoleView();
            try
            {
                _gameWorld = JsonFileReader.LoadGameWorld("C:\\Users\\rodin\\OneDrive\\Documenten\\GitHub\\avans-code\\deelopdracht-1-24-25-temple-of-doom-rodinvdberg_maxstaals-1\\Temple_of_doom\\Temple_of_doom\\TempleOfDoom.json") ?? CreateDefaultGameWorld();

                if (_gameWorld.CurrentRoom == null || _gameWorld.Player == null)
                {
                    Console.WriteLine("Debug: CurrentRoom or Player is missing.");
                    throw new NullReferenceException("GameWorld is missing essential components (CurrentRoom or Player). Check JSON or default world initialization.");
                }

                // Assign player start position
                _gameWorld.Player.Position = _gameWorld.CurrentRoom.GetPlayerStartPosition();

                // Start the game loop
                while (!_gameWorld.IsGameOver)
                {
                    _view.DisplayRoom(_gameWorld.CurrentRoom, _gameWorld.Player);
                    var command = _view.GetPlayerInput();
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

        private GameWorld CreateDefaultGameWorld()
        {
            Console.WriteLine("Loading default GameWorld.");
            var defaultRoom = new Room
            {
                Name = "Default Room",
                Items = new List<Item> { new SankaraStone {Power = 10}, new Key {Color = "Red"} },
                Doors = new List<Door>(),
                Layout = new char[,]
                {
                    { '#', '#', '#', '#', '#' },
                    { '#', 'X', '.', 'S', '#' },
                    { '#', '.', '#', '.', '#' },
                    { '#', 'K', '.', '.', '#' },
                    { '#', '#', '#', '#', '#' }
                }
            };

            return new GameWorld
            {
                Player = new Player { Lives = 3 },
                CurrentRoom = defaultRoom
            };
        }
    }
}
