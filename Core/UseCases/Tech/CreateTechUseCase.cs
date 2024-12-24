using Trackit.Core.Entities;
using Trackit.Core.Gateways;

namespace Trackit.Core.UseCases;

public class CreateTechUseCase(ITechGateway gateway)
{
  private readonly ITechGateway _gateway = gateway;

  public async Task Execute(string login, string name, string password, string phone, TechRole role, string? email)
  {
    var tech = new Tech(login, name, password, phone, role, email);
    await _gateway.CreateAsync(tech);
    return;
  }
}