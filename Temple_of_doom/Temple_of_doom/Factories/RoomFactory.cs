using Temple_of_doom.DTO;
using Temple_of_doom.Mappers;
using Temple_of_doom.Models;

namespace Temple_of_doom.Factories;

public class RoomFactory
{
    private readonly RoomMapper _roomMapper;

    public RoomFactory(RoomMapper roomMapper)
    {
        _roomMapper = roomMapper;
    }

    public Room CreateRoom(RoomDTO roomDTO)
    {
        return _roomMapper.MapToRoom(roomDTO);
    }
}