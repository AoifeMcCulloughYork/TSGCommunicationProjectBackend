using System;

namespace TSGCommunicationProjectBackend.Common.Dtos;

public class CommunicationTypeStatusDto
{
    public int TypeCode { get; set; }
    public string StatusCode { get; set; } = string.Empty;
    public string Description{ get; set; } = string.Empty;
}
