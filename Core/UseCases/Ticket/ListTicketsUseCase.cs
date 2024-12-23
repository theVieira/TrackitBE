using Trackit.Core.Entities;
using Trackit.Core.Gateways;

namespace Trackit.Core.UseCases;

public class ListTicketsUseCase(ITicketGateway gateway)
{
  private readonly ITicketGateway _gateway = gateway;

  public async Task<List<Ticket>> Execute()
  {
    var tickets = await _gateway.ListAsync();
    return tickets;
  }
}