using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.data.Models.Enemies
{
    public class HorizontalEnemy : Enemy
    {
        private int _direction = 1; // 1 = rechts, -1 = links

        public HorizontalEnemy(int x, int y, int minX, int maxX)
            : base(x, y, minX, y, maxX, y) { }

        public override void Move()
        {
            X += _direction;

            if (X >= MaxX || X <= MinX)
            {
                _direction *= -1; // Keer om als hij de grens bereikt
            }
        }
    }
}
