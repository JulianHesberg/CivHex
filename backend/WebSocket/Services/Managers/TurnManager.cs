using api.Models.Logic;
using backend.Models;
using backend.Models.states;
using backend.WebSocket.Dto;

namespace backend.WebSocket.Services.Managers;

public static class TurnManager
{
    public static void EndTurn(Guid playerId, Guid roomId)
    {
        GameState gameState = StateManager.FindGameStateByRoomId(roomId);
        if (StateManager.IsPlayersTurn(playerId, gameState))
        {
            gameState.TurnNumber += 1;
            RollDie(gameState);
            StateManager.UpdateRoomStateAndNotify(roomId, gameState);
        }

    }

    private static void RollDie(GameState gameState)
    {
        Random rnd = new Random();
        int rollNumber = rnd.Next(1, 7);
        CheckTilesAndGiveResources(rollNumber, gameState);
    }

    private static void CheckTilesAndGiveResources(int rollNumber, GameState gameState)
    {
        TileStatusChecker checker = new TileStatusChecker();
        foreach (var row in gameState.HexTilesList)
        {
            foreach (var tile in row)
            {
                if (TileNumberEqualsRollNumber(rollNumber, tile) && checker.IsTileOwned(tile))
                {
                    GiveResources(tile);
                }
            }
        }
    }

    private static bool TileNumberEqualsRollNumber(int rollNumber, HexTile tile)
    {
        if (rollNumber == tile.TileNumber)
        { 
            return true;
        }
           
        return false;
    }

    private static void GiveResources(HexTile tile)
    {
        switch (tile.GetTileType)
        {
            case TileType.Wood:
                tile.Owner.Wood += 1;
                break;
            case TileType.Stone:
                tile.Owner.Stone += 1;
                break;
            case TileType.Grain:
                tile.Owner.Grain += 1;
                break;
            case TileType.Sheep:
                tile.Owner.Sheep += 1;
                break;
        }
    }
    
    
    
}