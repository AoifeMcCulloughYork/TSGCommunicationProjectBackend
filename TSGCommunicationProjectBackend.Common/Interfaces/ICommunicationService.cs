using System;
using TSGCommunicationProjectBackend.Common.Dtos;
using TSGCommunicationProjectBackend.Common.Dtos.Requests;

namespace TSGCommunicationProjectBackend.Common.Interfaces;

public interface ICommunicationService
{
    Task<CommunicationDto?> GetCommunicationByIdAsync(Guid communicationId);
    Task<IEnumerable<CommunicationDto>> GetAllCommunicationsAsync();
    Task<CommunicationDto> CreateCommunicationAsync(CreateCommunicationRequest createCommunicationRequest);
    Task<CommunicationDto?> UpdateCommunicationAsync(Guid communicationId, UpdateCommunicationRequest updateCommunicationRequest);
    Task<bool?> DeleteCommunicationAsync(Guid communicationId);
    Task<List<CommunicationStatusHistoryItemDto>?> GetCommunicationStatusHistoryItems(Guid communicationId);
}
