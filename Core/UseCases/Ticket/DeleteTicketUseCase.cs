using Trackit.Core.Gateways;

namespace Trackit.Core.UseCases;
public class DeleteTicketUseCase(ITicketGateway gateway) {
  private readonly ITicketGateway _gateway = gateway;

  public async Task Execute(string id)
  {
    var ticket = await _gateway.FindById(id) ?? throw new ArgumentException("ticket not found");
    await _gateway.DeleteAsync(ticket);
    return;
  }
}