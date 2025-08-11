using System;
using TSGCommunicationProjectBackend.Common.Dtos;
using TSGCommunicationProjectBackend.Common.Dtos.Requests;

namespace TSGCommunicationProjectBackend.Common.Interfaces;

public interface ICommunicationTypesService
{
    Task<IEnumerable<CommunicationTypeDto>> GetAllTypesAsync();
    Task<CommunicationTypeDto?> GetTypeByIdAsync(int id);
    Task<CommunicationTypeDto> CreateTypeAsync(CreateCommunicationTypeRequest request);
    Task<bool> UpdateTypeAsync(int TypeCode, UpdateCommunicationTypeRequest request);
    Task<bool> DeleteTypeAsync(DeleteCommunicationTypeRequest request);
    //update type
    //delete type
}
