using Trackit.Core.Entities;
using Trackit.Core.Gateways;

namespace Trackit.Infra.Persistence;

public class TechRepository : ITechGateway
{
    public Task CreateAsync(Tech tech)
    {
        throw new NotImplementedException();
    }

    public Task<Tech?> FindByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}
