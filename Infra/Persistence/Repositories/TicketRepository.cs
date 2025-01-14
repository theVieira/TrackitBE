using Microsoft.EntityFrameworkCore;
using Trackit.Domain.Entities;
using Trackit.Domain.Interfaces;

namespace Trackit.Infra.Persistence.Repositories;

public class TicketRepository(AppDbContext context) : ITicket
{
    private AppDbContext _context = context;
    
    public async Task AddAsync(Ticket entity)
    {
        await _context.Tickets.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Ticket?> FindByIdAsync(Guid id)
    {
        var ticket= await _context
            .Tickets
            .Include(x => x.Client)
            .Include(x => x.CreatedBy)
            .FirstOrDefaultAsync(x => x.Id == id);
        return ticket;
    }

    public Task<List<Ticket>> ListAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Ticket entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Ticket>> ListByClientAsync(int skip, int take, TicketFilters filters, string clientName)
    {
        var tickets = await _context.Tickets
            .AsNoTracking()
            .Include(x => x.Client)
            .Include(x => x.CreatedBy)
            .Where(x => filters.Category.Contains(x.Category))
            .Where(x => filters.Status.Contains(x.Status))
            .Where(x => filters.Priority.Contains(x.Priority))
            .Where(x => clientName == x.Client.Name)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        
        return tickets;
    }

    public async Task<List<Ticket>> ListAsync(int skip, int take, TicketFilters filters)
    {
        var tickets = await _context.Tickets
            .AsNoTracking()
            .Include(x => x.Client)
            .Include(x => x.CreatedBy)
            .Where(x => filters.Category.Contains(x.Category))
            .Where(x => filters.Status.Contains(x.Status))
            .Where(x => filters.Priority.Contains(x.Priority))
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return tickets;
    }
}