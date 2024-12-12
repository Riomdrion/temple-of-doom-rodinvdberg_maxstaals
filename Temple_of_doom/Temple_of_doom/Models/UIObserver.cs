namespace Temple_of_doom.Models
{
    public class UIObserver
    {
        public void Update(string message)
        {
            Console.WriteLine($"UI Update: {message}");
        }
    }
}
