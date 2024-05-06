namespace backend.Models;
    
public class HexTile
{
    #region properties

        public TileType GetTileType { get; set; }
    
    public TileStatus GetTileStatus { get; set; }
        
    public int TileNumber { get; set; }
        
    public Player? Owner { get; set; }
    

    #endregion
    

    public HexTile(TileType tileType, TileStatus tileStatus, int tileNumber)
    {
        GetTileType = tileType;
        GetTileStatus = tileStatus;
        TileNumber = tileNumber;
        Owner = null;
    }
    
    
    
}