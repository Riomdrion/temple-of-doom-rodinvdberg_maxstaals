namespace Temple_of_doom.Models
{
    public abstract class Door : UIObserver
    {

        public void Update()
        {
            Console.WriteLine("Door has been notified and will open.");
            Open();
        }

        public abstract void Open();
    }
}
