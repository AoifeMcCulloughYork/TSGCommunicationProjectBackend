using System;
using TSGCommunicationProjectBackend.Data.Entities;

namespace TSGCommunicationProjectBackend.Data.Repositories.Interfaces;

public interface ICommunicationTypeStatusRepository
{
    Task<bool> IsStatusValidForType(int TypeCode, string StatusCode);
    Task<CommunicationTypeStatus> CreateCommunicationTypeStatus(int TypeCode, string StatusCode, string Description);
    Task<bool> DeleteCommunicationTypeStatus(CommunicationTypeStatus Cts);
    Task<CommunicationTypeStatus> UpdateCommunicationTypeStatus(CommunicationTypeStatus Cts);
    Task<IEnumerable<CommunicationTypeStatus>> GetValidStatusesForType(int typeCode);
    Task<CommunicationTypeStatus?> GetCommunicationTypeStatusAsync(int typeCode, string statusCode);
}
