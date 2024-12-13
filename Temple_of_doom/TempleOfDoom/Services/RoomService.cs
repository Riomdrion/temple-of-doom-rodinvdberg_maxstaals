using TempleOfDoom.DTO;
using TempleOfDoom.Factories;
using TempleOfDoom.Models;
using TempleOfDoom.Repositories;
using TempleOfDoom.Validators;

namespace TempleOfDoom.Services;

public class RoomService
{
    private readonly RoomRepository _roomRepository;
    private readonly RoomFactory _roomFactory;
    private readonly RoomValidator _roomValidator;

    public RoomService(RoomRepository roomRepository, RoomFactory roomFactory, RoomValidator roomValidator)
    {
        _roomRepository = roomRepository;
        _roomFactory = roomFactory;
        _roomValidator = roomValidator;
    }

    public void AddRoom(RoomDTO roomDTO)
    {
        if (_roomValidator.Validate(roomDTO))
        {
            var room = _roomFactory.CreateRoom(roomDTO);
            _roomRepository.AddRoom(room);
        }
        else
        {
            throw new ArgumentException("Room data is invalid.");
        }
    }

    public Room GetRoomById(int id)
    {
        return _roomRepository.GetRoomById(id);
    }

    public List<Room> GetAllRooms()
    {
        return _roomRepository.GetAllRooms();
    }
}
