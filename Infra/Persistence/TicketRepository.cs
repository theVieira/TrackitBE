using Microsoft.EntityFrameworkCore;
using Trackit.Core.Entities;
using Trackit.Core.Gateways;
using Trackit.Infra.Data;

namespace Trackit.Infra.Persistence;

public class TicketRepository(ApplicationContext context) : ITicketGateway
{
    private readonly ApplicationContext _context = context;

    public async Task CreateAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
        return;
    }

    public async Task DeleteAsync(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
        return;
    }

    public async Task<Ticket?> FindById(string id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        return ticket;
    }

    public Task<List<Ticket>> ListAsync()
    {
        var tickets = _context.Tickets
            .Include(t => t.Client)
            .Include(t => t.Open)
            .Include(t => t.Attachments)
            .Include(t => t.Notes)
            .Include(t => t.Feedbacks)
            .ToListAsync();
        return tickets;
    }
}