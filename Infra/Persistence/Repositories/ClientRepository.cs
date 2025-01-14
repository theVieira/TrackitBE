using Microsoft.AspNetCore.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Trackit.Domain.Entities;
using Trackit.Domain.Interfaces;

namespace Trackit.Infra.Persistence.Repositories;

public class ClientRepository(AppDbContext context) : IClient
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Client entity)
    {
        await _context.Clients.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return;
    }

    public async Task<Client?> FindByIdAsync(Guid id)
    {
        var client = await _context.Clients.FindAsync(id);
        
        return client;
    }

    public async Task<List<Client>> ListAsync(int skip, int take)
    {
        var clients =
            await _context.Clients
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        
        return clients;
    }

    public Task UpdateAsync(Client entity)
    {
        throw new NotImplementedException();
    }
}