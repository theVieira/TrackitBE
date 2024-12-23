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

    public Task<List<Ticket>> ListAsync()
    {
        var tickets = _context.Tickets.ToListAsync();
        return tickets;
    }
}