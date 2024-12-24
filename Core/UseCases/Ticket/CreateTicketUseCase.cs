using Trackit.Core.Entities;
using Trackit.Core.Gateways;

namespace Trackit.Core.UseCases;

public class CreateTicketUseCase(
    ITicketGateway ticketGateway,
    IClientGateway clientGateway,
    ITechGateway techGateway,
    IBotMessage botMessage
    )
{
  private readonly ITicketGateway _ticketGateway = ticketGateway;
  private readonly IClientGateway _clientGateway = clientGateway;
  private readonly ITechGateway _techGateway = techGateway;
  private readonly IBotMessage _botMessage = botMessage;

  public async Task Execute(string clientId, string techId, TicketCategory category, TicketPriority priority, string description)
  {
    var client = await _clientGateway.FindByIdAsync(clientId) ?? throw new ArgumentException("client not found!");
        
    var tech = await _techGateway.FindByIdAsync(techId) ?? throw new ArgumentException("tech not found!");

    var ticket = new Ticket(client, tech, category, priority, description);

    await _ticketGateway.CreateAsync(ticket);

    await _botMessage.SendMessage($"Novo chamado criado\nCliente: {client.Name}\nCriado por: {tech.Name}\nDescrição: {ticket.Description}\nPrioridade: {ticket.Priority}\nCategoria: {ticket.Category}");

    return;
  }
}