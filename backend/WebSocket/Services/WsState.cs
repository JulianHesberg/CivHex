using api.Models.Helpers;
using api.Models.Logic;
using backend.Models;
using backend.Models.states;
using Fleck;

namespace backend.WebSocket.State;

public class WsState
{
    public static Dictionary<Guid, IWebSocketConnection> Connections = new();
    public static Dictionary<Guid, IWebSocketConnection> Queue = new();
    
    public static Dictionary<Guid, Guid> PlayersRooms = new();

    //Change string to GameState after testing.
    public static Dictionary<Guid, GameState> RoomsState = new();
    

    public static bool AddConnection(IWebSocketConnection ws)
    {
        return Connections.TryAdd(ws.ConnectionInfo.Id, ws);
    }

    public static void AddToQueue(IWebSocketConnection ws)
    {
        Queue.TryAdd(ws.ConnectionInfo.Id, ws);
        CheckForNewGame();
    }

    public static void CheckForNewGame()
    {
        while (Queue.Count >= 2)
        {
            var player1 = Queue.First().Key;
            var player2 = Queue.Skip(1).First().Key;
            
            AddPlayersToRooms(Queue[player1] ,Queue[player2]);
            
            Queue.Remove(player1);
            Queue.Remove(player2);
        }
    }

    public static void AddPlayersToRooms(IWebSocketConnection ws1, IWebSocketConnection ws2)
    {
        var roomId = Guid.NewGuid();
        GameState state = GameStateHelper.NewGame(roomId, ws1.ConnectionInfo.Id, ws2.ConnectionInfo.Id);
        
        RoomsState.TryAdd(roomId, state);
        PlayersRooms.TryAdd(ws1.ConnectionInfo.Id, roomId);
        PlayersRooms.TryAdd( ws2.ConnectionInfo.Id, roomId);
        

        foreach (var id in PlayersRooms)
        {
            if (roomId == id.Value)
            {
                Connections[id.Key].Send(state.Serialize());
            }
        }
    }

    public static void PurchaseTile(Guid roomId, Guid playerId, int rowIndex, int columnIndex)
    {
        GameState gameState = null;
        Player owner = null;
        foreach (var room in RoomsState )
        {
            if (room.Key == roomId)
            {
                gameState = room.Value;
            }
        }

        foreach (var player in gameState.PlayersList)
        {
            if (player.WsId == playerId)
            {
                owner = player;
            }
        }
        TileStatusChecker checker = new TileStatusChecker();

        if (checker.IsTilePurchasable(gameState.HexTilesList, owner, rowIndex, columnIndex))
        {
            gameState.HexTilesList[rowIndex][columnIndex].Owner = owner;
            gameState.HexTilesList[rowIndex][columnIndex].GetTileStatus = TileStatus.Owned;
            
        }

        if (RoomsState.ContainsKey(roomId))
        {
            RoomsState[roomId] = gameState;
        }
        foreach (var connection in PlayersRooms)
        {
            if (connection.Value == roomId)
            {
                Connections[connection.Key].Send(gameState.Serialize());
            }
        }
    }
}