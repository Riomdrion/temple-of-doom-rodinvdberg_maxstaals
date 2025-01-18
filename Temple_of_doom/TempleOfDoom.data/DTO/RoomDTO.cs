﻿namespace TempleOfDoom.data.DTO;

public class RoomDto
{
    public int Id { get; set; }
    public List<ItemDto> Items { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public List<FloorTileDTO> FloorTile { get; set; }

}