namespace Temple_of_doom.DTO;

public class RoomDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ItemDTO> Items { get; set; }
    public List<DoorDTO> Doors { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}