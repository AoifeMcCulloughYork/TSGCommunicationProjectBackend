using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using Microsoft.Extensions.Logging;
using TSGCommunicationProjectBackend.Common.Dtos;
using TSGCommunicationProjectBackend.Common.Dtos.Requests;
using TSGCommunicationProjectBackend.Common.Interfaces;
using TSGCommunicationProjectBackend.Data.Entities;
using TSGCommunicationProjectBackend.Data.Repositories.Interfaces;

namespace TSGCommunicationProjectBackend.Service;

public class CommunicationTypeService : ICommunicationTypesService
{
    private readonly ICommunicationTypeRepository _communicationTypeRepository;
    private readonly ICommunicationTypeStatusRepository _communicationTypeStatusRepository;
    private readonly ILogger<CommunicationTypeService> _logger;

    public CommunicationTypeService(
        ICommunicationTypeRepository communicationTypeRepository,
        ICommunicationTypeStatusRepository communicationTypeStatusRepository,
        ILogger<CommunicationTypeService> logger
    )
    {
        _communicationTypeRepository = communicationTypeRepository;
        _communicationTypeStatusRepository = communicationTypeStatusRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<CommunicationTypeDto>> GetAllTypesAsync()
    {
        var types = await _communicationTypeRepository.GetAllAsync();
        var allTypes = new List<CommunicationTypeDto>();
        foreach (var type in types)
        {
            var Dto = new CommunicationTypeDto
            {
                TypeCode = type.TypeCode,
                DisplayName = type.DisplayName
            };
            allTypes.Add(Dto);
        }
        return allTypes;
    }
    public async Task<CommunicationTypeDto?> GetTypeByIdAsync(int typeCode)
    {
        var type = await _communicationTypeRepository.GetTypeAsync(typeCode);
        _logger.LogInformation("found type {type}", type);
        if (type == null)
        {
            _logger.LogWarning("type not found");
            return null;
        }
        return new CommunicationTypeDto
        {
            DisplayName = type.DisplayName,
            TypeCode = type.TypeCode
        };
    }

    public async Task<bool> UpdateTypeAsync(int typeCode, UpdateCommunicationTypeRequest request)
    {
        var type = await _communicationTypeRepository.GetTypeAsync(typeCode);
        if (type == null)
        {
            return false;
        }
        type.DisplayName = request.DisplayName;
        await _communicationTypeRepository.UpdateAsync(type);
        return true;
    }
    public async Task<bool> DeleteTypeAsync(DeleteCommunicationTypeRequest request)
    {
        var type = await _communicationTypeRepository.GetTypeAsync(request.TypeCode);
        if (type == null)
        {
            return false;
        }
        type.Active = false;
        await _communicationTypeRepository.UpdateAsync(type);
        return true;
    }
    public async Task<bool> RestoreTypeAsync(int typeCode)
    {
        var type = await _communicationTypeRepository.GetTypeAsync(typeCode);
        if (type == null)
        {
            return false;
        }
        type.Active = true;
        await _communicationTypeRepository.UpdateAsync(type);
        return true;
    }
    public async Task<IEnumerable<CommunicationTypeStatus>?> GetValidStatusesForTypeAsync(int typeCode)
    {
        var type = await _communicationTypeRepository.GetTypeAsync(typeCode);
        if (type == null)
        {
            return null;
        }
        List<CommunicationTypeStatus> ValidStatuses = (List<CommunicationTypeStatus>)await _communicationTypeStatusRepository.GetValidStatusesForType(typeCode);
        return ValidStatuses;
    }
    public async Task<CommunicationTypeDto> CreateTypeAsync(CreateCommunicationTypeRequest request)
    {
        var type = new CommunicationType
        {
            DisplayName = request.DisplayName,
            Active = true,
        };
        await _communicationTypeRepository.CreateAsync(type);
        return new CommunicationTypeDto
        {
            DisplayName = type.DisplayName,
            TypeCode = type.TypeCode
        };
    }
    public async Task<bool> UpdateType(UpdateCommunicationTypeRequest request)
    {
        var type = await _communicationTypeRepository.GetTypeAsync(request.TypeCode);
        if (type == null)
        {
            return false;
        }
        type.DisplayName = request.DisplayName;
        await _communicationTypeRepository.UpdateAsync(type);
        return true;
    }
    public async Task<bool> UpdateTypeStatuses(UpdateCommunicationTypeStatusesRequest request)
    {
        var type = await _communicationTypeRepository.GetTypeAsync(request.TypeCode);
        if (type == null)
        {
            return false;
        }
        foreach (var newCommunicationTypeStatus in request.Statuses)
        {
            if (await _communicationTypeStatusRepository.IsStatusValidForType(newCommunicationTypeStatus.TypeCode, newCommunicationTypeStatus.StatusCode) == false)
            {
                await _communicationTypeStatusRepository.CreateCommunicationTypeStatus(newCommunicationTypeStatus.TypeCode, newCommunicationTypeStatus.StatusCode, newCommunicationTypeStatus.Description);
            }

        }
        foreach (var deletableCommunicationTypeStatus in request.DeletableStatuses)
        {
            var Cts = await _communicationTypeStatusRepository.GetCommunicationTypeStatusAsync(deletableCommunicationTypeStatus.TypeCode, deletableCommunicationTypeStatus.StatusCode);
            if (Cts != null)
            {
                await _communicationTypeStatusRepository.DeleteCommunicationTypeStatus(Cts);
            }
            
        }
        return true;
    }

}

