using System;

namespace TSGCommunicationProjectBackend.Common.Dtos.Requests;

public class UpdateCommunicationTypeStatusesRequest
{
    public int TypeCode { get; set; }
    public List<CommunicationTypeStatusDto> Statuses { get; set; } = new();
    public List<CommunicationTypeStatusDto> DeletableStatuses { get; set; } = new();
}