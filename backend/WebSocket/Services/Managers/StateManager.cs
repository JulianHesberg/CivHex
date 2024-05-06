using backend.Models;
using backend.Models.states;

namespace backend.WebSocket.Services.Managers;

public static class StateManager
{
    public static Player FindPlayerById(GameState gameState, Guid playerId)
    {
        return gameState.PlayersList.FirstOrDefault(player => player.WsId == playerId);
    }
    
    public static GameState FindGameStateByRoomId(Guid roomId)
    {
        return WsState.RoomsState.ContainsKey(roomId) ? WsState.RoomsState[roomId] : null; 
    }
    
    public static bool IsPlayersTurn(Guid playerId, GameState gameState)
    {
        if (gameState.TurnNumber % 2 == 1 && gameState.PlayersList[0].WsId == playerId)
        {
            return true;
        }
        if (gameState.TurnNumber % 2 == 0 && gameState.PlayersList[1].WsId == playerId)
        {
            return true;
        }

        return false;

    }
    
    public static void UpdateRoomStateAndNotify(Guid roomId, GameState gameState)
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