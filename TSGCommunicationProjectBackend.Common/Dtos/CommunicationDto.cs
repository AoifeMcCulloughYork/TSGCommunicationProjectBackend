using System;

namespace TSGCommunicationProjectBackend.Common.Dtos;

public class CommunicationDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public int TypeCode { get; set; }
    public string CurrentStatus { get; set; } = String.Empty;
    public DateTime LastUpdatedUtc { get; set; }
    
}
