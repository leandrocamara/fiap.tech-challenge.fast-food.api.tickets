using Entities.SeedWork;

namespace Adapters.Gateways;

public interface IRepository<T> where T : IAggregatedRoot
{
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<T?> GetById(Guid id);
}