﻿using Fleck;

namespace backend.WebSocket.State;

public class WsState
{
    public static Dictionary<Guid, IWebSocketConnection> Connections = new();
    public static Dictionary<Guid, IWebSocketConnection> Queue = new();
    
    public static Dictionary<Guid, Guid> PlayersRooms = new();

    //Change string to GameState after testing.
    public static Dictionary<Guid, string> RoomsState = new();
    

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
        var gameState = "New State!!";
        var roomId = Guid.NewGuid();

        RoomsState.TryAdd(roomId, gameState);
        PlayersRooms.TryAdd(ws1.ConnectionInfo.Id, roomId);
        PlayersRooms.TryAdd( ws2.ConnectionInfo.Id, roomId);
        

        foreach (var id in PlayersRooms)
        {
            if (roomId == id.Value)
            {
                Connections[id.Key].Send(gameState);
            }
        }
    }
    /**
    public static void BroadcastToRoom(int room, string message)
    {
        if (Rooms.TryGetValue(room, out var guids))
        {
            foreach (var guid in guids)
            {
                if (Connections.TryGetValue(guid, out var ws))
                    ws.Connection.Send(message);

            }
        }
    }
    **/
}