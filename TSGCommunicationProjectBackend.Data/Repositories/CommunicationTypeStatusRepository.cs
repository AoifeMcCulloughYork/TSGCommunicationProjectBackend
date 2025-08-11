using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TSGCommunicationProjectBackend.Data.Contexts;
using TSGCommunicationProjectBackend.Data.Entities;
using TSGCommunicationProjectBackend.Data.Repositories.Interfaces;

namespace TSGCommunicationProjectBackend.Data.Repositories;

public class CommunicationTypeStatusRepository : ICommunicationTypeStatusRepository
{
    private readonly ApplicationDbContext _context;
    public CommunicationTypeStatusRepository(ApplicationDbContext context, ILogger<CommunicationTypeRepository> logger)
    {
        _context = context;
    }
    public async Task<CommunicationTypeStatus> CreateCommunicationTypeStatus(int typeCode, string statusCode, string description)
    {
        var NewTypeStatus = new CommunicationTypeStatus
        {
            TypeCode = typeCode,
            StatusCode = statusCode,
            Description = description
        };
        _context.CommunicationTypeStatuses.Add(NewTypeStatus);
        await _context.SaveChangesAsync();
        return NewTypeStatus;
    }

    public async Task<bool> DeleteCommunicationTypeStatus(CommunicationTypeStatus Cts)
    {
        try
        {
            _context.CommunicationTypeStatuses.Remove(Cts);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> IsStatusValidForType(int TypeCode, string StatusCode)
    {
        var ValidTypeStatus = await _context.CommunicationTypeStatuses.FirstOrDefaultAsync(Cts => Cts.TypeCode == TypeCode && Cts.StatusCode == StatusCode);
        if (ValidTypeStatus == null)
        {
            return false;
        }
        return true;
    }

    public async Task<CommunicationTypeStatus> UpdateCommunicationTypeStatus(CommunicationTypeStatus Cts)
    {
        _context.CommunicationTypeStatuses.Update(Cts);
        await _context.SaveChangesAsync();
        return Cts;
    }
    public async Task<IEnumerable<CommunicationTypeStatus>> GetValidStatusesForType(int typeCode)
    {
        return await _context.CommunicationTypeStatuses
        .Where(Cts => Cts.TypeCode == typeCode)
        .Include(Cts => Cts.StatusCode)
        .ToListAsync();
    }
    public async Task<CommunicationTypeStatus?> GetCommunicationTypeStatusAsync(int typeCode, string statusCode)
    {
        return await _context.CommunicationTypeStatuses.FirstOrDefaultAsync(cts => cts.TypeCode == typeCode && cts.StatusCode == statusCode);
    }
}
