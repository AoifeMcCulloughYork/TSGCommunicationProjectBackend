using System;
using System.Runtime.CompilerServices;
using Azure.Core;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TSGCommunicationProjectBackend.Common.Dtos;
using TSGCommunicationProjectBackend.Common.Dtos.Requests;
using TSGCommunicationProjectBackend.Common.Interfaces;
using TSGCommunicationProjectBackend.Data.Entities;
using TSGCommunicationProjectBackend.Data.Repositories.Interfaces;
using static TSGCommunicationProjectBackend.Common.Statuses;



namespace TSGCommunicationProjectBackend.Service;

public class CommunicationService : ICommunicationService
{
    private readonly ICommunicationRepository _communicationRepository;
    private readonly ILogger<CommunicationService> _logger;
    private readonly ICommunicationTypeRepository _communicationTypeRepository;
    private readonly ICommunicationTypeStatusRepository _communicationTypeStatusRepository;
    private readonly IMemberRepository _memberRepository;
    public CommunicationService(
        ICommunicationRepository communicationRepository,
        ILogger<CommunicationService> logger,
        ICommunicationTypeRepository communicationTypeRepository,
        ICommunicationTypeStatusRepository communicationTypeStatusRepository,
        IMemberRepository memberRepository
    )
    {
        _communicationRepository = communicationRepository;
        _logger = logger;
        _communicationTypeRepository = communicationTypeRepository;
        _communicationTypeStatusRepository = communicationTypeStatusRepository;
        _memberRepository = memberRepository;
    }
    public async Task<CommunicationDto> CreateCommunicationAsync(CreateCommunicationRequest createCommunicationRequest)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(createCommunicationRequest.Title))
            {
                throw new ArgumentException("Must have Title", nameof(createCommunicationRequest));
            }
            var member = await _memberRepository.GetMemberAsync(createCommunicationRequest.MemberId);
            var communicationType = await _communicationTypeRepository.GetTypeAsync(createCommunicationRequest.TypeCode);
            if (member == null || communicationType == null)
            {
                throw new ArgumentException("Invalid member or communication type");
            }
            var communication = new Communication
            {
                Title = createCommunicationRequest.Title,
                MemberId = createCommunicationRequest.MemberId,
                TypeCode = createCommunicationRequest.TypeCode,
                CurrentStatus = Created,
                Active = true
            };
            var AddedCommunication = await _communicationRepository.CreateAsync(communication);
            AddedCommunication.MemberId = member.Id;
            return MapToResponse(AddedCommunication);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating communication");
            throw;
        }
    }

    public async Task<bool?> DeleteCommunicationAsync(Guid communicationId)
    {
        try
        {
            var communication = await _communicationRepository.GetCommunicationAsync(communicationId);
            if (communication == null)
            {
                _logger.LogInformation("Tried to delete communication which doesn't exist");
                return false;
            }
            await _communicationRepository.DeleteCommuncationAsync(communicationId);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting communication {communicationId}", communicationId);
            throw;
        }
    }

    public async Task<IEnumerable<CommunicationDto>> GetAllCommunicationsAsync()
    {
        try
        {
            _logger.LogInformation("getting all communications");
            var communications = await _communicationRepository.GetAllCommunicationsAsync();
            return communications.Select(c => MapToResponse(c)).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all communications");
            throw;
        }
    }

    public async Task<CommunicationDto?> GetCommunicationByIdAsync(Guid communicationId)
    {
        try
        {
            _logger.LogInformation("Getting communcation {communcationId}", communicationId);
            var communication = await _communicationRepository.GetCommunicationAsync(communicationId);
            if (communication == null)
            {
                _logger.LogWarning("No such communication found");
                return null;
            }
            return MapToResponse(communication);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting communication {communicationId}", communicationId);
            throw;
        }
    }

    public async Task<List<CommunicationStatusHistoryItemDto>?> GetCommunicationStatusHistoryItems(Guid communicationId)
    {
        try
        {
            _logger.LogInformation("Getting history for communication {communicationId}", communicationId);
            var communication = await _communicationRepository.GetCommunicationAsync(communicationId);
            if (communication == null)
            {
                _logger.LogInformation("Tried to get history for communication which doesn't exist");
                return null;
            }
            var history = await _communicationRepository.GetCommunicationStatusHistoriesAsync(communicationId);
            var CommunicationStatusHistoryItemDtos = history.Select(h => new CommunicationStatusHistoryItemDto
            {
                CommunicationId = h.CommunicationId,
                Id = h.Id,
                StatusCode = h.StatusCode,
                OccurredUtc = h.OccurredUtc

            }).ToList();

            _logger.LogInformation("Found histories for communcation {communicationId}", communicationId);
            return CommunicationStatusHistoryItemDtos;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting communcation history for {communicationId}", communicationId);
            throw;
        }
    }

    public async Task<CommunicationDto?> UpdateCommunicationAsync(Guid communicationId, UpdateCommunicationRequest updateCommunicationRequest)
    {
        try
        {
            var communication = await _communicationRepository.GetCommunicationAsync(communicationId);
            if (communication == null)
            {
                _logger.LogInformation("tried to update a communication that doesn't exist");
                return null;
            }
            if (!updateCommunicationRequest.Title.IsNullOrEmpty())
            {
                communication.Title = updateCommunicationRequest.Title;
            }
            if (updateCommunicationRequest.TypeCode != communication.TypeCode)
            {
                communication.TypeCode = updateCommunicationRequest.TypeCode;
            }
            await _communicationRepository.UpdateCommunicationAsync(communication);
            return MapToResponse(communication);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating communcation {communcationId}", communicationId);
            throw;
        }
    }

    private static CommunicationDto MapToResponse(Communication communication)
    {
        return new CommunicationDto
            {
                TypeCode = communication.TypeCode,
                Title = communication.Title,
                CurrentStatus = communication.CurrentStatus,
                LastUpdatedUtc = DateTime.UtcNow,
                Id = communication.Id
            };
    }
}
