using backend.Models;
using backend.Models.states;

namespace api.Models.Helpers;

public static class GameStateHelper
{
    public static GameState NewGame(Guid roomId, Guid ws1Id, Guid ws2Id)
    {
        List<List<int>> numbers = new List<List<int>>
        {
            new List<int> {1, 2, 3, 4, 2, 1},
            new List<int> {6, 4, 5, 1, 3, 6},
            new List<int> {4, 3, 1, 4, 6, 3},
            new List<int> {5, 3, 5, 2, 1, 4},
            new List<int> {6, 2, 4, 6, 2, 5},
            new List<int> {3, 6, 1, 5, 3, 2}
        };

        List<List<TileType>> tileTypes = new List<List<TileType>>
        {
            new List<TileType> {TileType.Grain, TileType.Stone, TileType.Wood, TileType.Sheep, TileType.Grain, TileType.Stone},
            new List<TileType> {TileType.Sheep, TileType.Grain, TileType.Stone, TileType.Wood, TileType.Sheep, TileType.Wood},
            new List<TileType> {TileType.Wood, TileType.Stone, TileType.Grain, TileType.Stone ,TileType.Grain, TileType.Stone},
            new List<TileType> {TileType.Stone, TileType.Sheep, TileType.Sheep, TileType.Wood,TileType.Sheep, TileType.Wood},
            new List<TileType> {TileType.Stone, TileType.Wood, TileType.Grain, TileType.Grain, TileType.Stone, TileType.Grain},
            new List<TileType> {TileType.Grain, TileType.Wood, TileType.Sheep, TileType.Sheep, TileType.Wood, TileType.Sheep}
        };
        
        int startingGold = 10;
        List<List<HexTile>> hexList = new List<List<HexTile>>();

        foreach (var row in tileTypes)
        {
            var hexRow = new List<HexTile>();

            foreach (var tileType in row)
            {
                hexRow.Add(new HexTile(tileType, TileStatus.Unowned, numbers[hexList.Count][hexRow.Count]));
            }

            hexList.Add(hexRow);
        }

        
        List<Player> players = new List<Player>
        {
            new Player("player1", startingGold, ws1Id),
            new Player("player2", startingGold, ws2Id)
        };
        
        //Remove later, testing purchase of tiles
        hexList[0][1].Owner = players[0];
        hexList[0][1].GetTileStatus = TileStatus.Owned;
        
        hexList[5][4].Owner = players[1];
        hexList[5][4].GetTileStatus = TileStatus.Owned;
        
        GameState state = new GameState(players, hexList, 1, roomId);

        return state;
    }
}