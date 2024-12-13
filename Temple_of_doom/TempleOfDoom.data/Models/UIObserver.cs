namespace TempleOfDoom.data.Models;

public class UiObserver
{
    public void Update(string message)
    {
        Console.WriteLine($"UI Update: {message}");
    }
}