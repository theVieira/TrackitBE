using Trackit.Core.Entities;

namespace Trackit.Core.Gateways;

public interface ITicketGateway
{
  Task CreateAsync(Ticket ticket);
}