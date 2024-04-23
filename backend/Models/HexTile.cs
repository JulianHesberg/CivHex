namespace DefaultNamespace;

public class HexTile
{
    public TileType GetTileType{
        get;
        set;
    }

    public TileStatus GetTileStatus
    {
        get;
        set;
    }

    public HexTile(TileType tileType, TileStatus tileStatus)
    {
        this.GetTileType = tileType;
        this.GetTileStatus = tileStatus
    }
    
    
    
}