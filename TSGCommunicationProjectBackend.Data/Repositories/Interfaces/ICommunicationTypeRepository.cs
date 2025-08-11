using System;
using TSGCommunicationProjectBackend.Data.Entities;
namespace TSGCommunicationProjectBackend.Data.Repositories.Interfaces;

public interface ICommunicationTypeRepository
{
    Task<IEnumerable<CommunicationType>> GetAllAsync();
    Task<CommunicationType?> GetTypeAsync(int typeCode);
    Task<CommunicationType> CreateAsync(CommunicationType type);
    Task<CommunicationType> UpdateAsync(CommunicationType type);
    Task<bool> DeleteAsync(int id); 
}
