using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Data.Repository;

public class MemberRepository(DataContext _context) : IMemberRepository
{
    public async Task<Member?> GetMemberByIdAsync(string id)
    {
        return await _context.Members.FindAsync(id);
    }
    public async Task<IReadOnlyList<Member>> GetMembersAsync()
    {
        return await _context.Members.ToListAsync();
    }
    public async Task<IReadOnlyList<Photo>> GetPhotosByMemberIdAsync(string memberId)
    {
        return await _context.Members
        .Where(p => p.Id == memberId)
        .SelectMany(p => p.Photos)
        .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
    public void Update(Member member)
    {
        _context.Entry(member).State = EntityState.Modified;
    }
}
