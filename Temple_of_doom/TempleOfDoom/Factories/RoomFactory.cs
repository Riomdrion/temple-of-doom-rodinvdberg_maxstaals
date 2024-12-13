using TempleOfDoom.DTO;
using TempleOfDoom.Mappers;
using TempleOfDoom.Models;

namespace TempleOfDoom.Factories;

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