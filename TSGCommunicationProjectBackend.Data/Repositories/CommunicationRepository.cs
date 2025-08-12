using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TSGCommunicationProjectBackend.Data.Contexts;
using TSGCommunicationProjectBackend.Data.Entities;
using TSGCommunicationProjectBackend.Data.Repositories.Interfaces;

namespace TSGCommunicationProjectBackend.Data.Repositories;

public class CommunicationRepository : ICommunicationRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CommunicationRepository> _logger;
    public CommunicationRepository(ApplicationDbContext context, ILogger<CommunicationRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Communication> CreateAsync(Communication communication)
    {
        communication.CreatedUtc = DateTime.UtcNow;
        communication.LastUpdatedUtc = DateTime.UtcNow;
        _context.Communications.Add(communication);
        await _context.SaveChangesAsync();
        return communication;
    }

    public async Task<bool> DeleteCommuncationAsync(Guid id)
    {
        try
        {
            //FirstAsync is used here because it will throw a InvalidOperationException if not found
            //could alternatively use FirstOrDefault and check for null but same effect
            var communication = await _context.Communications.FirstAsync(c => c.Id == id);
            communication.Active = false;
            communication.LastUpdatedUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<IEnumerable<Communication>> GetAllCommunicationsAsync()
    {
        return await _context.Communications.Where(c => c.Active == true)
        .OrderByDescending(c => c.LastUpdatedUtc)
        .ToListAsync();
    }

    public async Task<Communication?> GetCommunicationAsync(Guid id)
    {
        try
        {
            return await _context.Communications
            .Where(c => c.Active == true)
            .FirstOrDefaultAsync(c => c.Id == id && c.Active == true);
        }
        catch
        {
            throw;
        }
    }

    public Task<IEnumerable<Communication>> GetMemberCommunicationsAsync(Guid memberId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateCommunicationAsync(Communication communication)
    {
        try
        {
            communication.LastUpdatedUtc = DateTime.UtcNow;
            _context.Communications.Update(communication);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateCommunicationStatusAsync(Guid communicationId, string newStatusCode)
    {
        try
        {
            var communication = await _context.Communications.FindAsync(communicationId);
            if (communication == null)
            {
                _logger.LogInformation("communication {communicationId} came back null", communicationId);
                return false;
            }
            communication.CurrentStatus = newStatusCode;
            communication.LastUpdatedUtc = DateTime.UtcNow;
            var NewHistory = new CommunicationStatusHistory
            {
                StatusCode = newStatusCode,
                OccurredUtc = DateTime.UtcNow,
                CommunicationId = communicationId
            };
            _context.CommunicationStatusHistories.Add(NewHistory);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating communication {communicationId}", communicationId);
            return false;
        }
    }

    public async Task<List<CommunicationStatusHistory>> GetCommunicationStatusHistoriesAsync(Guid communicationId)
    {
        _logger.LogInformation("getting history for communication {communcationId}", communicationId);
        var communcation = await _context.Communications.FindAsync(communicationId);
        if (communcation == null)
        {
            _logger.LogInformation("no such communication found");
            //will return an empty list
        }
        return await _context.CommunicationStatusHistories
        .Where(c => c.CommunicationId == communicationId)
        .OrderByDescending(c => c.OccurredUtc)
        .ToListAsync();
    }
}
