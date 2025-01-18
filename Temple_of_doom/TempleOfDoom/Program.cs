using TempleOfDoom.Controllers;
using TempleOfDoom.Data;

namespace TempleOfDoom;

internal class Program
{
    private static void Main(string[] args)
    {
        var useXml = args.Contains("--xml"); 
        IDataReader dataReader = useXml ? new XmlDataReader() : new JsonDataReader();

        var gameController = new GameController(dataReader);
        gameController.StartGame();
    }
}