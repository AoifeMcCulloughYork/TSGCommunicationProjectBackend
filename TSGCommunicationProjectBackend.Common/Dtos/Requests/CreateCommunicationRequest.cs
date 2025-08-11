using System;
namespace TSGCommunicationProjectBackend.Common.Dtos.Requests;

public class CreateCommunicationRequest
{
    public string Title { get; set; } = String.Empty;
    public int TypeCode { get; set; }
    public Guid MemberId { get; set; }
}
