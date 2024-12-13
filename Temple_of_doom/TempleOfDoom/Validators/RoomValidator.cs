using TempleOfDoom.DTO;

namespace TempleOfDoom.Validators;

public class RoomValidator
{
    public bool Validate(RoomDTO roomDTO)
    {
        if (roomDTO.Id <= 0)
            return false;

        if (string.IsNullOrEmpty(roomDTO.Name))
            return false;

        if (roomDTO.Width <= 0 || roomDTO.Height <= 0)
            return false;

        return true;
    }
}