using System.Text.Json;

namespace backend.Models.states;

public class GameState
{
    #region Properties
    public int TurnNumber { get; set; }
    public Guid RoomId { get; set; }
    public List<Player> PlayersList { get; set; }
    public List<List<HexTile>> HexTilesList { get; set; }
    
    #endregion

    
    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }

    public GameState Deserialize(string data)
    {
       return JsonSerializer.Deserialize<GameState>(data)!;
    }

    public GameState(List<Player> playersList, List<List<HexTile>> hexTilesList, int turnNumber, Guid roomId)
    {
        PlayersList = playersList;
        HexTilesList = hexTilesList;
        TurnNumber = turnNumber;
        RoomId = roomId;
    }
}