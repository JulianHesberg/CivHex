using System.Collections.Concurrent;
using api.Models.Helpers;
using backend.Models.states;
using backend.WebSocket.Services.Managers;
using Fleck;

namespace backend.WebSocket.Services;

public class WsState
{
    public static Dictionary<Guid, IWebSocketConnection> Connections = new();
    public static Dictionary<Guid, IWebSocketConnection> Queue = new();
    
    public static Dictionary<Guid, Guid> PlayersRooms = new();
    public static Dictionary<Guid, GameState> RoomsState = new();
    

    public static bool AddConnection(IWebSocketConnection ws)
    {
        return Connections.TryAdd(ws.ConnectionInfo.Id, ws);
    }

    public static void AddToQueue(IWebSocketConnection ws)
    {
        QueueManager.AddToQueue(ws, Queue);
    }
    

    public static void AddPlayersToRooms(IWebSocketConnection ws1, IWebSocketConnection ws2)
    {

    }

    public static void PurchaseTile(Guid roomId, Guid playerId, int rowIndex, int columnIndex)
    {
        PurchaseTileManager.PurchaseTile(roomId, playerId, rowIndex, columnIndex);
    }
}