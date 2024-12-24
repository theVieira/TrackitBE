using Trackit.Core.Entities;
using Trackit.Core.Gateways;

namespace Trackit.Core.UseCases;

public class ListTechUseCase(ITechGateway gateway)
{
  private readonly ITechGateway _gateway = gateway;

  public async Task<List<Tech>> Execute(int skip, int take)
  {
    var techs = await _gateway.ListAsync(skip, take);
    return techs;
  }
}