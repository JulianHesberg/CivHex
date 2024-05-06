using lib;

namespace backend.WebSocket.Dto;

public class ClientWantsToPurchaseTileDto : BaseDto
{
    public Guid roomId { get; set; }
    public Guid playerId { get; set; }
    public int rowIndex { get; set; }
    public int columnIndex { get; set; }
}