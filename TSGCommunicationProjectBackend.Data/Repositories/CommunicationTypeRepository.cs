using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TSGCommunicationProjectBackend.Data.Contexts;
using TSGCommunicationProjectBackend.Data.Entities;
using TSGCommunicationProjectBackend.Data.Repositories.Interfaces;

namespace TSGCommunicationProjectBackend.Data.Repositories;

public class CommunicationTypeRepository : ICommunicationTypeRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CommunicationTypeRepository> _logger;
    public CommunicationTypeRepository(ApplicationDbContext context, ILogger<CommunicationTypeRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CommunicationType> CreateAsync(CommunicationType type)
    {
        _context.CommunicationTypes.Add(type);
        await _context.SaveChangesAsync();
        return type;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var type = await GetTypeAsync(id);
        if (type == null)
        {
            return false;
        }
        type.Active = false;
        _context.CommunicationTypes.Update(type);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<CommunicationType>> GetAllAsync()
    {
        return await _context.CommunicationTypes.Where(t => t.Active).ToListAsync();
    }

    public async Task<CommunicationType?> GetTypeAsync(int typeCode)
    {
        return await _context.CommunicationTypes.FirstOrDefaultAsync(t => t.TypeCode == typeCode);
    }

    public async Task<CommunicationType> UpdateAsync(CommunicationType type)
    {
        _context.CommunicationTypes.Update(type);
        await _context.SaveChangesAsync();
        return type;
    }
}
