namespace backend.Models.states;

public class GameState
{
    public List<Player> Players { get; set; }
    public List<HexTile> HexTiles { get; set; }
    public int TurnNumber { get; set; }

    public string Serialize()
    {
        //TODO
        return null;
    }

    public static GameState Deserialize(string data)
    {
        //TODO
        return null;
    }
}