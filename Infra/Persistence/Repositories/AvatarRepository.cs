using Trackit.Domain.Entities;
using Trackit.Domain.Interfaces;

namespace Trackit.Infra.Persistence.Repositories;

public class AvatarRepository(AppDbContext context) : IAvatar
{
    private readonly AppDbContext _context = context;
    
    public async Task AddAsync(Avatar entity)
    {
        await _context.Avatars.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public Task<Avatar?> FindByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseListResponse<Avatar>> ListAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Avatar entity)
    {
        throw new NotImplementedException();
    }
}