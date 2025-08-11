using System;

namespace TSGCommunicationProjectBackend.Common.Dtos.StatusDtos;

public class CommunicationStatusChangedEvent
{
    public string NewStatusCode { get; set; } = string.Empty;
}
