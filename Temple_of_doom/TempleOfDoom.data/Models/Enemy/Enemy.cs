using TempleOfDoom.data.Models.FloorTiles;
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


        public void Move(Room currentRoom)
        {
            int deltaX = 0, deltaY = 0;

            // Beweging op basis van type
            if (Type.ToLower() == "horizontal")
            {
                deltaX = MovingForward ? 1 : -1;
            }
            else if (Type.ToLower() == "vertical")
            {
                deltaY = MovingForward ? 1 : -1;
            }

            // Bereken nieuwe positie
            var newPosition = new Position(Position.X + deltaX, Position.Y + deltaY);

            // Controleer of de nieuwe positie geldig is
            if (currentRoom.IsPositionWalkable(newPosition) &&
                newPosition.X >= MinX && newPosition.X <= MaxX &&
                newPosition.Y >= MinY && newPosition.Y <= MaxY)
            {
                Position = newPosition; // Verplaats naar de nieuwe positie
            }
            else
            {
                MovingForward = !MovingForward; // Keer om als de positie niet geldig is
                return; // Stop verdere verwerking
            }

            // Controleer of de vijand op een ijstegel staat
            while (currentRoom.GetFloorTileAt(Position) is IceTile)
            {
                // Beweeg verder in dezelfde richting over de ijstegel
                newPosition = new Position(Position.X + deltaX, Position.Y + deltaY);

                if (currentRoom.IsPositionWalkable(newPosition) &&
                    newPosition.X >= MinX && newPosition.X <= MaxX &&
                    newPosition.Y >= MinY && newPosition.Y <= MaxY)
                {
                    Position = newPosition; // Update positie
                }
                else
                {
                    break; // Stop als de nieuwe positie niet geldig is
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