using Microsoft.EntityFrameworkCore;
using Trackit.Core.Entities;
using Trackit.Core.Gateways;
using Trackit.Infra.Data;

namespace Trackit.Infra.Persistence;

public class ClientRepository(ApplicationContext context) : IClientGateway
{
    private readonly ApplicationContext _context = context;
    public async Task CreateAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
        return;
    }

    public async Task<Client?> FindByIdAsync(string id)
    {
        if(Guid.TryParse(id, out Guid Id))
        {
            var client = await _context.Clients.FindAsync(Id);
            return client;
        }

        return null;
    }

    public async Task<List<Client>> ListAsync(int skip, int take)
    {
        var clients = await _context.Clients.Skip(skip).Take(take).ToListAsync();
        return clients;
    }
}