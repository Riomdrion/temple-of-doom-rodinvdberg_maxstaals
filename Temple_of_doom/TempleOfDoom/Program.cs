using TempleOfDoom.Controllers;

namespace TempleOfDoom
{
    class Program
    {
        static void Main(string[] args)
        {
            GameController gameController = new GameController();
            gameController.StartGame();
        }
    }
}
