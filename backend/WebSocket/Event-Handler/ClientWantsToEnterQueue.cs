using backend.WebSocket.Dto;
using backend.WebSocket.State;
using Fleck;
using lib;

namespace backend.WebSocket.Event_Handler;

public class ClientWantsToEnterQueue(ClientConnections clientConnections) : BaseEventHandler<ClientWantsToEnterQueueDto>
{
    public override Task Handle(ClientWantsToEnterQueueDto dto, IWebSocketConnection socket)
    {
        WsState.AddToQueue(socket);
        return Task.CompletedTask;
    }
}