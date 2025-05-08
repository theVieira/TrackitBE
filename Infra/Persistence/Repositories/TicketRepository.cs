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
            .AsSplitQuery()
            .Include(x => x.Client)
            .Include(x => x.CreatedBy)
            .Include(x => x.Attachments)
            .Include(x => x.Progress)
            .Include(x => x.Finish)
            .Include(x => x.Reopen)
            .Include(x => x.Notes)
            .FirstOrDefaultAsync(x => x.Id == id);
        return ticket;
    }

    public Task<BaseListResponse<Ticket>> ListAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Ticket entity)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseListResponse<Ticket>> ListByClientAsync(int skip, int take, TicketFilters filters, string clientName)
    {
        var tickets = 
            await _context.Tickets
            .AsSplitQuery()
            .Include(x => x.Client)
            .Include(x => x.CreatedBy)
            .Include(x => x.Attachments)
            .Include(x => x.Progress)
            .Include(x => x.Finish)
            .Include(x => x.Reopen)
            .Include(x => x.Notes)
            .Where(x => filters.Category.Contains(x.Category))
            .Where(x => filters.Status.Contains(x.Status))
            .Where(x => filters.Priority.Contains(x.Priority))
            .Where(x => clientName == x.Client.Name)
            .ToListAsync();
        
        return new(tickets.Skip(skip).Take(take).ToList(), tickets.Count);
    }

    public async Task<BaseListResponse<Ticket>> ListAsync(int skip, int take, TicketFilters filters)
    {
        var tickets = await _context.Tickets
            .AsSplitQuery()
            .Include(x => x.Client)
            .Include(x => x.CreatedBy)
            .Include(x => x.Attachments)
            .Include(x => x.Progress)
            .Include(x => x.Finish)
            .Include(x => x.Reopen)
            .Include(x => x.Notes)
            .Where(x => filters.Category.Contains(x.Category))
            .Where(x => filters.Status.Contains(x.Status))
            .Where(x => filters.Priority.Contains(x.Priority))
            .ToListAsync();

        return new(tickets.Skip(skip).Take(take).ToList(), tickets.Count);
    }
}