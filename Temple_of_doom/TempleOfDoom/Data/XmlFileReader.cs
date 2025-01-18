

using System.Xml.Serialization;
using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Map;
using TempleOfDoom.Factory;

namespace TempleOfDoom.Data
{
    public static class XmlFileReader
    {
        public static GameWorld LoadGameWorld(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            // Deserialize XML file
            var serializer = new XmlSerializer(typeof(GameWorldXmlData));
            GameWorldXmlData xmlData;
            using (var stream = File.OpenRead(filePath))
            {
                xmlData = (GameWorldXmlData)serializer.Deserialize(stream);
            }

            // Process rooms
            var roomsData = xmlData.Rooms;
            var connectionsData = xmlData.Connections;

            // Ensure nested data is correctly initialized
            foreach (var connection in connectionsData)
            {
                connection.Doors ??= new List<DoorDto>();
                connection.Ladder ??= null;
            }

            foreach (var roomDto in roomsData)
            {
                roomDto.Items ??= new List<ItemDto>();
                roomDto.FloorTile ??= new List<FloorTileDTO>();
                roomDto.Enemies ??= new List<EnemyDto>();
            }

            // Create rooms and connections
            var rooms = RoomFactory.CreateRooms(roomsData, connectionsData);

            // Create player
            var player = new Player(
                xmlData.Player.Lives,
                new Position(xmlData.Player.StartX, xmlData.Player.StartY)
            );

            // Set start room
            var startRoomId = xmlData.Player.StartRoomId;
            player.currentRoom = rooms.FirstOrDefault(r => r.Id == startRoomId);

            // Create GameWorld
            return new GameWorld(player, rooms);
        }
    }

    [XmlRoot("GameWorld")]
    public class GameWorldXmlData
    {
        [XmlElement("Rooms")]
        public List<RoomDto> Rooms { get; set; }

        [XmlElement("Connections")]
        public List<ConnectionDto> Connections { get; set; }

        [XmlElement("Player")]
        public PlayerXmlData Player { get; set; }
    }

    public class PlayerXmlData
    {
        [XmlElement("Lives")]
        public int Lives { get; set; }

        [XmlElement("StartX")]
        public int StartX { get; set; }

        [XmlElement("StartY")]
        public int StartY { get; set; }

        [XmlElement("StartRoomId")]
        public int StartRoomId { get; set; }
    }
}
