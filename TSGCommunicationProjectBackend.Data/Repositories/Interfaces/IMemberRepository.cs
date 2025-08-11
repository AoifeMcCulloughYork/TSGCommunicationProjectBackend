using System;
using TSGCommunicationProjectBackend.Data.Entities;

namespace TSGCommunicationProjectBackend.Data.Repositories.Interfaces;

public interface IMemberRepository
{
    Task<Member?> GetMemberAsync(Guid id);
    Task<Member> CreateAsync(Member member);
    Task<Member> UpdateAsync(Member member);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> RestoreAsync(Guid id);
}
