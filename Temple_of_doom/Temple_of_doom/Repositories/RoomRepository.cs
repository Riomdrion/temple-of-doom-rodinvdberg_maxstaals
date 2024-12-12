using Temple_of_doom.Models;

namespace Temple_of_doom.Repositories;

public class RoomRepository
{
    private readonly List<Room> _rooms = new List<Room>();

    public void AddRoom(Room room)
    {
        _rooms.Add(room);
    }

    public Room GetRoomById(int id)
    {
        return _rooms.FirstOrDefault(r => r.Id == id);
    }

    public List<Room> GetAllRooms()
    {
        return _rooms;
    }
}
