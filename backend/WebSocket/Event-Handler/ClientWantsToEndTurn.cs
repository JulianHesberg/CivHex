using backend.WebSocket.Dto;
using backend.WebSocket.Services;
using Fleck;
using lib;

namespace backend.WebSocket.Event_Handler;

public class ClientWantsToEndTurn : BaseEventHandler<ClientWantsToEndTurnDto>
{
    public override Task Handle(ClientWantsToEndTurnDto dto, IWebSocketConnection socket)
    {
        WsState.EndTurn(dto.playerId, dto.roomId);
        return Task.CompletedTask;
    }
}