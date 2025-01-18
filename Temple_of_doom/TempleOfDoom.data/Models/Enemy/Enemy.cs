using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Enemy
{
    public class Enemy(string type, Position position, int minX, int maxX, int minY, int maxY, int lives = 1)
    {
        private string Type { get; } = type;
        public Position Position { get; private set; } = position;
        private int MinX { get; } = minX;
        private int MaxX { get; } = maxX;
        private int MinY { get; } = minY;
        private int MaxY { get; } = maxY;
        private int Lives { get; set; } = lives;
        private bool MovingForward { get; set; } = true;


        public void Move()
        {
            int deltaX = 0, deltaY = 0;

            if (Type.ToLower() == "horizontal")
            {
                deltaX = MovingForward ? 1 : -1;
                Position = new Position(Position.X + deltaX, Position.Y);

                if (Position.X >= MaxX || Position.X <= MinX)
                {
                    MovingForward = !MovingForward;
                }
            }
            else if (Type.ToLower() == "vertical")
            {
                deltaY = MovingForward ? 1 : -1;
                Position = new Position(Position.X, Position.Y + deltaY);

                if (Position.Y >= MaxY || Position.Y <= MinY)
                {
                    MovingForward = !MovingForward;
                }
            }
        }

        public void TakeDamage()
        {
            Lives--;
        }

        public bool IsDead()
        {
            return Lives <= 0;
        }
    }
}