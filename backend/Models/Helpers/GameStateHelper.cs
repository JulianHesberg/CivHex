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

        List<Player> players = new List<Player>();
        int startingGold = 10;
        List<List<HexTile>> hexList = new List<List<HexTile>>();

        for (int i = 0; i < 6; i++)
        {
            hexList.Add(new List<HexTile>()); // Initialize inner list

            for (int j = 0; j < 6; j++)
            {
                hexList[i].Add(new HexTile(tileTypes[i][j], TileStatus.Unowned, numbers[i][j])); // Add new HexTile to the inner list
            }
        }

        Player player1 = new Player("player1", startingGold, ws1Id);
        Player player2 = new Player("player2", startingGold, ws2Id);
        
        players.Add(player1);
        players.Add(player2);


        GameState state = new GameState(players, hexList, 1, roomId);

        return state;
    }
}