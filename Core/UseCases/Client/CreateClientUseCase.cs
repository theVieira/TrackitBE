using Trackit.Core.Entities;
using Trackit.Core.Gateways;

namespace Trackit.Core.UseCases;

public class CreateClientUseCase(IClientGateway gateway)
{
  private readonly IClientGateway _gateway = gateway;

  public async Task Execute(string name, string cnpj, string phone, string? email)
  {
    var client = new Client(name, cnpj, phone, email);
    await _gateway.CreateAsync(client);
    return;
  }
}