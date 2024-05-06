using lib;

namespace backend.WebSocket.Dto;

public class ClientWantsToEndTurnDto : BaseDto
{
    public Guid roomId { get; set; }
    public Guid playerId { get; set; }
}