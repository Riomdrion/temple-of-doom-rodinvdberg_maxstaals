using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temple_of_doom.data.Model;

namespace Temple_of_doom.Model
{
    public class PressurePlate
    {
        private readonly List<UIObserver> observers = new();

        public void Attach(UIObserver observer) => observers.Add(observer);
        public void Detach(UIObserver observer) => observers.Remove(observer);
        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }

        public void Activate()
        {
            Console.WriteLine("PressurePlate activated!");
            Notify(); // Alle observers worden geïnformeerd
        }
    }

}
