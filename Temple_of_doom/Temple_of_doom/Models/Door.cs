namespace Temple_of_doom.Models
{
    public abstract class Door : UIObserver
    {
        public string Id { get; set; }
        public bool IsOpen { get; set; }

        public abstract bool CanOpen(Player player);

        public void Open()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
        }
    }
}
