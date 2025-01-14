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
        var tech = await _context.Techs.FindAsync(id);
        
        return tech;
    }

    public async Task<List<Tech>> ListAsync(int skip, int take)
    {
        var techs =
            await _context.Techs
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        
        return techs;
    }

    public Task UpdateAsync(Tech entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Tech?> FindByEmailAsync(string email)
    {
        var tech = await _context.Techs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);

        return tech;
    }
}