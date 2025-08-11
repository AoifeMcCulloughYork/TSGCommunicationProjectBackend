using System;

namespace TSGCommunicationProjectBackend.Common.Dtos.Requests;

public class UpdateCommunicationTypeRequest
{
    public int TypeCode { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public List<string> AllowedStatuses = new();
}
