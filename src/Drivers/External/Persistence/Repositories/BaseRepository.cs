using Adapters.Gateways;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Entities.SeedWork;

namespace External.Persistence.Repositories
{
    public abstract class BaseRepository<T>(IDynamoDBContext context) : IRepository<T> where T : class, IAggregatedRoot
    {
        
        public async Task Add(T entity) => await context.SaveAsync<T>(entity);    
        public async Task Delete(T entity) => await context.DeleteAsync<T>(entity);        
        public async Task<T?> GetById(Guid pk, Guid sk) => await context.LoadAsync<T?>(pk, sk);        
        public async Task Update(T entity) => await context.SaveAsync<T>(entity);               
    }
}
