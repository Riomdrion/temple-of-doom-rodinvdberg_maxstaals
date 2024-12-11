using Temple_of_doom.Controllers;

namespace Temple_of_doom
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
