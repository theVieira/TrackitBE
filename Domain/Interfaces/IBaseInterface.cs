namespace Trackit.Domain.Interfaces;

public interface IBaseInterface<T>
{
    Task AddAsync(T entity);
    Task<T?> FindByIdAsync(Guid id);
    Task<List<T>> ListAsync(int skip, int take);
    Task UpdateAsync(T entity);
}