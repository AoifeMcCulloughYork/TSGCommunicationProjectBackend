using System;
using TSGCommunicationProjectBackend.Data.Entities;

namespace TSGCommunicationProjectBackend.Data.Repositories.Interfaces;

public interface ICommunicationRepository
{
    Task<Communication> CreateAsync(Communication communication);
    Task<Communication?> GetCommunicationAsync(Guid id);
    Task<IEnumerable<Communication>> GetAllCommunicationsAsync();
    Task<bool> UpdateCommunicationAsync(Communication communication);
    Task<bool> DeleteCommuncationAsync(Guid id);
    Task<bool> UpdateCommunicationStatusAsync(Guid communicationId, string newStatusCode);
    Task<IEnumerable<Communication>> GetMemberCommunicationsAsync(Guid memberId);
    Task<List<CommunicationStatusHistory>> GetCommunicationStatusHistoriesAsync(Guid communicationId);
}
