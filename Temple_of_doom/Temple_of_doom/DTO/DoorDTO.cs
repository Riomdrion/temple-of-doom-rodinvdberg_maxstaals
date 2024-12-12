namespace Temple_of_doom.DTO;

public class DoorDTO
{
    public int Id { get; set; }
    public int TargetRoomId { get; set; }
    public object Type { get; }
}
