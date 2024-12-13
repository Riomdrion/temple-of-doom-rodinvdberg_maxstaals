using TempleOfDoom.Controllers;

namespace TempleOfDoom;

internal class Program
{
    private static void Main(string[] args)
    {
        var gameController = new GameController();
        gameController.StartGame();
    }
}