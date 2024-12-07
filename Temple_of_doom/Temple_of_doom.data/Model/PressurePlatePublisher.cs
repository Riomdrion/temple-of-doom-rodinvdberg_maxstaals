using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temple_of_doom.data.Model
{
    public class PressurePlatePublisher
    {
        private List<UIObserver> observers = new();

        public void Attach(UIObserver observer) => observers.Add(observer);
        public void Detach(UIObserver observer) => observers.Remove(observer);
        public void Notify() => observers.ForEach(o => o.Update());
    }
}
