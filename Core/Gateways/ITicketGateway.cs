using Trackit.Core.Entities;

namespace Trackit.Core.Gateways;

public interface ITicketGateway
{
  Task CreateAsync(Ticket ticket);
  Task<List<Ticket>> ListAsync();
  Task DeleteAsync(Ticket ticket);
  Task<Ticket?> FindById(string id); 
}