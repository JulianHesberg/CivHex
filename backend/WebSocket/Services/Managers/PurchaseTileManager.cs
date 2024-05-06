using api.Models.Logic;
using backend.Models;
using backend.Models.states;

namespace backend.WebSocket.Services.Managers;

public static class PurchaseTileManager
{
    public static void PurchaseTile (Guid roomId, Guid playerId, int rowIndex, int columnIndex)
    {

        GameState gameState = StateManager.FindGameStateByRoomId(roomId);
        Player owner = StateManager.FindPlayerById(gameState, playerId);
        
        if (gameState != null && owner != null && StateManager.IsPlayersTurn(playerId, gameState))
        {
            if (gameState.TurnNumber <= 6)
            {
                TakeStartingTurn(gameState, owner, rowIndex, columnIndex, roomId);
                return;
            }

            TileStatusChecker checker = new TileStatusChecker();
            if (checker.IsTilePurchasable(gameState.HexTilesList, owner, rowIndex, columnIndex))
            {
                PurchaseTileAndUpdateGameState(gameState, owner, rowIndex, columnIndex);
                StateManager.UpdateRoomStateAndNotify(roomId, gameState);
            }
        }
    }

    private static void PurchaseTileAndUpdateGameState(GameState gameState, Player owner, int rowIndex, int columnIndex)
    {
        gameState.HexTilesList[rowIndex][columnIndex].Owner = owner;
        gameState.HexTilesList[rowIndex][columnIndex].GetTileStatus = TileStatus.Owned;
    }

    

    

    

    private static void TakeStartingTurn(GameState gameState, Player owner, int rowIndex, int columnIndex, Guid roomId)
    {
        TileStatusChecker checker = new TileStatusChecker();
        
       if (gameState.TurnNumber <= 2 && !checker.IsTileOwned(gameState.HexTilesList[rowIndex][columnIndex]))
        {
            PurchaseTileAndUpdateGameState(gameState, owner, rowIndex, columnIndex);
            ForceEndTurn(roomId, gameState);
        }
        
        if (gameState.TurnNumber>2 && checker.IsTilePurchasable(gameState.HexTilesList, owner, rowIndex, columnIndex))
        {
            PurchaseTileAndUpdateGameState(gameState, owner, rowIndex, columnIndex);
            ForceEndTurn(roomId, gameState);
        }
        
    }

    private static void ForceEndTurn(Guid roomId, GameState gameState)
    {
        gameState.TurnNumber += 1;
        StateManager.UpdateRoomStateAndNotify(roomId, gameState);
    }
    
}