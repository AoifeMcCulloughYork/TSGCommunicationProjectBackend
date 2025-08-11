using System;
namespace TSGCommunicationProjectBackend.Common.Dtos.StatusDtos;

public class CommunicationCreatedEvent : CommunicationEventBaseDto
{
    //CommunicationId and Timestamp already handled in CommunicationEventBaseDto
    public string Title { get; set; } = String.Empty;
    public int TypeCode { get; set; }
}
