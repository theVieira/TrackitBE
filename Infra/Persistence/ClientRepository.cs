using Trackit.Core.Entities;
using Trackit.Core.Gateways;

namespace Trackit.Infra.Persistence;

public class ClientRepository : IClientGateway
{
    public Task CreateAsync(Client client)
    {
        throw new NotImplementedException();
    }

    public Task<Client?> FindByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}