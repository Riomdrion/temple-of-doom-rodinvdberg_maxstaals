namespace Temple_of_doom.Models
{
    public class PressurePlatePublisher
    {
        private List<UIObserver> observers = new();

        public void Attach(UIObserver observer) => observers.Add(observer);
        public void Detach(UIObserver observer) => observers.Remove(observer);
        public void Notify() => observers.ForEach(o => o.Update());
    }
}
