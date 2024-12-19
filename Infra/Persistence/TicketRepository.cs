using Trackit.Core.Entities;
using Trackit.Core.Gateways;

namespace Trackit.Infra.Persistence;

public class TicketRepository : ITicketGateway
{
    public Task CreateAsync(Ticket ticket)
    {
        throw new NotImplementedException();
    }
}