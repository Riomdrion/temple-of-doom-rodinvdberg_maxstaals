using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.data.Models.Enemies
{
    public abstract class Enemy
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public int MinX { get; protected set; }
        public int MinY { get; protected set; }
        public int MaxX { get; protected set; }
        public int MaxY { get; protected set; }

        protected Enemy(int x, int y, int minX, int minY, int maxX, int maxY)
        {
            X = x;
            Y = y;
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }

        public abstract void Move(); // Elke vijand bepaalt zijn eigen beweging
    }
}
