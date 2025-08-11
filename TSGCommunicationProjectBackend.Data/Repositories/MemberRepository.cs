using System;
using Microsoft.EntityFrameworkCore;
using TSGCommunicationProjectBackend.Data.Contexts;
using TSGCommunicationProjectBackend.Data.Entities;
using TSGCommunicationProjectBackend.Data.Repositories.Interfaces;

namespace TSGCommunicationProjectBackend.Data.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext _context;
    public MemberRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Member> CreateAsync(Member member)
    {
        member.CreatedUtc = DateTime.UtcNow;
        member.LastUpdatedUtc = DateTime.UtcNow;

        _context.Members.Add(member);
        await _context.SaveChangesAsync();
        return member;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var member = await GetMemberAsync(id);
        if (member == null)
        {
            return false;
        }
        member.Active = false;
        member.LastUpdatedUtc = DateTime.UtcNow;
        _context.Members.Update(member);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Member?> GetMemberAsync(Guid id)
    {
        return await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<bool> RestoreAsync(Guid id)
    {
        var member = await GetMemberAsync(id);
        if (member == null)
        {
            return false;
        }
        member.Active = true;
        member.LastUpdatedUtc = DateTime.UtcNow;
        _context.Members.Update(member);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Member> UpdateAsync(Member member)
    {
        member.LastUpdatedUtc = DateTime.UtcNow;
        _context.Members.Update(member);
        await _context.SaveChangesAsync();
        return member;
    }
}
