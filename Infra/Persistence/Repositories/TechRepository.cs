using Microsoft.EntityFrameworkCore;
using Trackit.Domain.Entities;
using Trackit.Domain.Interfaces;

namespace Trackit.Infra.Persistence.Repositories;

public class TechRepository(AppDbContext context) : ITech
{
    private readonly AppDbContext _context = context;
    
    public async Task AddAsync(Tech entity)
    {
        await _context.Techs.AddAsync(entity);
        await _context.SaveChangesAsync();

        return;
    }

    public async Task<Tech?> FindByIdAsync(Guid id)
    {
        var tech =
            await _context.Techs
                .Include(x => x.Avatar)
                .FirstOrDefaultAsync(x => x.Id == id);
        
        return tech;
    }

    public async Task<BaseListResponse<Tech>> ListAsync(int skip, int take)
    {
        var techs =
            await _context.Techs
                .Include(x => x.Avatar)
                .AsNoTracking()
                .ToListAsync();
        
        return new(
            techs.Skip(skip).Take(take).ToList(),
            techs.Count
        );
    }

    public async Task UpdateAsync(Tech entity)
    {
        _context.Techs.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Tech?> FindByEmailAsync(string email)
    {
        var tech = await _context.Techs
            .Include(x => x.Avatar)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);

        return tech;
    }
}