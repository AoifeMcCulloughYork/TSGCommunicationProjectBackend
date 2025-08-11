using System;

namespace TSGCommunicationProjectBackend.Common.Dtos.Requests;

public class CreateCommunicationTypeRequest
{
    public string DisplayName { get; set; } = string.Empty;
    public List<string> AllowedStatuses { get; set; } = new();
    public int TypeCode { get; set; }
}
