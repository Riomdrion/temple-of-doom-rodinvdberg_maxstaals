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
                _gameWorld = JsonFileReader.LoadGameWorld("../../../TempleOfDoom.json");

                // if (_gameWorld.CurrentRoom == null )
                // {
                //     Console.WriteLine("Debug: CurrentRoom is missing.");
                //     throw new NullReferenceException("GameWorld is missing essential components (CurrentRoom). Check JSON or default world initialization.");
                // }
                // if (_gameWorld.Player == null)
                // {
                //     Console.WriteLine("Debug: Player is missing.");
                //     throw new NullReferenceException("GameWorld is missing essential components (Player). Check JSON or default world initialization.");
                // }
                
                // Assign player start position
                _gameWorld.Player.Position = _gameWorld.Player.GetPlayerStartPosition();

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
            if (string.IsNullOrEmpty(command)) return;

            if (command == "quit")
            {
                Console.WriteLine("Exiting game.");
                Environment.Exit(0);
            }
            else if (command == "up" || command == "down" || command == "left" || command == "right")
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
