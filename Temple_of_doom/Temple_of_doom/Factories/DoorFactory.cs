
using Temple_of_doom.Models;

namespace Temple_of_doom.Factories
{
    public static class DoorFactory
    {
        public static Door CreateDoor(string type, string id, string condition = null)
        {
            switch (type)
            {
                case "colored":
                    return new ColoredDoor { Id = id, KeyColor = condition };
                case "toggle":
                    return new ToggleDoor { Id = id };
                case "locked":
                    return new LockedDoor { Id = id, RequiredKeyColor = condition };
                case "alarmed":
                    return new AlarmedDoor { Id = id };
                default:
                    throw new ArgumentException("Invalid door type");
            }
        }
    }
}
