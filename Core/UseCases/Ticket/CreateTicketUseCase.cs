using Trackit.Core.Entities;
using Trackit.Core.Gateways;

namespace Trackit.Core.UseCases;

public class CreateTicketUseCase(
    ITicketGateway ticketGateway,
    IClientGateway clientGateway,
    ITechGateway techGateway
    )
{
  private readonly ITicketGateway _ticketGateway = ticketGateway;
  private readonly IClientGateway _clientGateway = clientGateway;
  private readonly ITechGateway _techGateway = techGateway;

  public async Task Execute(string clientId, string techId, TicketCategory category, TicketPriority priority, string description)
  {
    var client = await _clientGateway.FindByIdAsync(clientId) ?? throw new ArgumentException("client not found!");
        
    var tech = await _techGateway.FindByIdAsync(techId) ?? throw new ArgumentException("tech not found!");

    var ticket = new Ticket(client, tech, category, priority, description);

    await _ticketGateway.CreateAsync(ticket);

    return;
  }
}