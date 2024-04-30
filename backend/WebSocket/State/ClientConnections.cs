using Fleck;
namespace backend.WebSocket.State;


public class ClientConnections
{
    public List<IWebSocketConnection> Connections = new List<IWebSocketConnection>();

    public void AddConnectionToPool(IWebSocketConnection socket)
    {
        Connections.Add(socket);
    }

    public void RemoveConnectionFromPool(IWebSocketConnection socket)
    {
        Connections.Remove(socket);
    }

    public List<IWebSocketConnection> GetClientConnections()
    {
        return Connections;
    }
}