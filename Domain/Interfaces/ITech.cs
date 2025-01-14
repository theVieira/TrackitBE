using Trackit.Domain.Entities;

namespace Trackit.Domain.Interfaces;

public interface ITech : IBaseInterface<Tech>
{
    Task<Tech?> FindByEmailAsync(string email);
}