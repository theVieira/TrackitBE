using Microsoft.OpenApi.Any;
using Trackit.Core.Entities;

namespace Trackit.Core.Gateways;

public interface IClientGateway
{
  Task CreateAsync(Client client);
  Task<Client?> FindByIdAsync(string id);
}