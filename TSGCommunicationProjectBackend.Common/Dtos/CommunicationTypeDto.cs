using System;

namespace TSGCommunicationProjectBackend.Common.Dtos;

public class CommunicationTypeDto
{
    public int TypeCode { get; set; }
    public string DisplayName { get; set; } = string.Empty;
}
