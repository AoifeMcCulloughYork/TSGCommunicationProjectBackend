using System;

namespace TSGCommunicationProjectBackend.Common.Dtos.Requests;

public class UpdateCommunicationRequest
{
    public Guid CommunicationId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int TypeCode { get; set; } //communication type updating is likely extremely rare
}
