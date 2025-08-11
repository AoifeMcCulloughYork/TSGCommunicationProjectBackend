using System;

namespace TSGCommunicationProjectBackend.Common.Dtos;

public class CommunicationStatusHistoryItemDto
{
    public Guid Id { get; set; }
    public Guid CommunicationId { get; set; }
    public string StatusCode { get; set; } = String.Empty;
    public DateTime OccurredUtc { get; set; }
}
