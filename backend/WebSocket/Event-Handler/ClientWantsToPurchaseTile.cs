using backend.WebSocket.Dto;
using backend.WebSocket.State;
using Fleck;
using lib;

namespace backend.WebSocket.Event_Handler;

public class ClientWantsToPurchaseTile : BaseEventHandler<ClientWantsToPurchaseTileDto>
{
    public override Task Handle(ClientWantsToPurchaseTileDto dto, IWebSocketConnection socket)
    {
        WsState.PurchaseTile(dto.roomId, dto.playerId, dto.rowIndex, dto.columnIndex);
        return Task.CompletedTask;
    }
}