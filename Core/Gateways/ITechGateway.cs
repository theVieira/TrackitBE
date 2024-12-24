using Trackit.Core.Entities;

namespace Trackit.Core.Gateways;

public interface ITechGateway
{
  Task CreateAsync(Tech tech);
  Task<Tech?> FindByIdAsync(string id);
  Task<List<Tech>> ListAsync(int skip, int take);
}