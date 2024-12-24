using Trackit.Core.Entities;
using Trackit.Core.Gateways;

namespace Trackit.Core.UseCases;

public class ListClientUseCase(IClientGateway gateway)
{
  private readonly IClientGateway _gateway = gateway;

  public async Task<List<Client>> Execute(int skip, int take)
  {
    var clients = await _gateway.ListAsync(skip, take);
    return clients;
  }
}