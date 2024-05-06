using backend.Models;

namespace api.Models.Logic;

public class TileStatusChecker
{
    public bool IsTilePurchasable(List<List<HexTile>> gameBoard, Player player, int rowIndex, int columnIndex)
    {
        if (gameBoard[rowIndex][columnIndex].GetTileStatus != TileStatus.Unowned)
        {
            return false;
        }
        

        List<List<int>> neighbors = new List<List<int>>
        {
            new List<int>{ -1, 0 }, new List<int>{ 1, 0 },
            new List<int>{ 0, -1 }, new List<int>{ 1, -1 },
            new List<int>{ 0, 1 }, new List<int>{ 1, 1 }
        };

        foreach (var neighbor in neighbors)
        {
            int neighborRow = rowIndex + neighbor[0];
            int neighborColumn = columnIndex + neighbor[1];

            if (neighborRow >= 0 && neighborRow < gameBoard.Count && neighborColumn >= 0 &&
                neighborColumn < gameBoard[neighborRow].Count)
            {
                if (gameBoard[neighborRow][neighborColumn].Owner == player)
                    return true;
            }
        }

        return false;
    }

    public bool IsTileOwned(HexTile tile)
    {
        if (tile.GetTileStatus == TileStatus.Owned)
        {
            return true;
        }

        return false;
    }

}