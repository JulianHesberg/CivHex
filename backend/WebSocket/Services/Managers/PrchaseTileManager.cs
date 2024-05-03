using api.Models.Logic;
using backend.Models;
using backend.Models.states;

namespace backend.WebSocket.Services.Managers;

public static class PurchaseTileManager
{
    public static void PurchaseTile (Guid roomId, Guid playerId, int rowIndex, int columnIndex)
    {

        GameState gameState = FindGameStateByRoomId(roomId);
        Player owner = FindPlayerById(gameState, playerId);

        if (gameState != null && owner != null)
        {
            TileStatusChecker checker = new TileStatusChecker();
            if (checker.IsTilePurchasable(gameState.HexTilesList, owner, rowIndex, columnIndex))
            {
                PurchaseTileAndUpdateGameState(gameState, owner, rowIndex, columnIndex);
                UpdateRoomStateAndNotify(roomId, gameState);
            }
        }
    }

    private static GameState FindGameStateByRoomId(Guid roomId)
    {
        return WsState.RoomsState.ContainsKey(roomId) ? WsState.RoomsState[roomId] : null; 
    }

    private static Player FindPlayerById(GameState gameState, Guid playerId)
    {
        return gameState.PlayersList.FirstOrDefault(player => player.WsId == playerId);
    }

    private static void PurchaseTileAndUpdateGameState(GameState gameState, Player owner, int rowIndex, int columnIndex)
    {
        gameState.HexTilesList[rowIndex][columnIndex].Owner = owner;
        gameState.HexTilesList[rowIndex][columnIndex].GetTileStatus = TileStatus.Owned;
    }

    private static void UpdateRoomStateAndNotify(Guid roomId, GameState gameState)
    {
        WsState.RoomsState[roomId] = gameState;
        NotifyPlayers(roomId, gameState);
    }

    private static void NotifyPlayers(Guid roomId, GameState gameState)
    {
        foreach (var connection in WsState.PlayersRooms)
        {
            if (connection.Value == roomId)
            {
                WsState.Connections[connection.Key].Send(gameState.Serialize());
            }
        }
    }
    
}