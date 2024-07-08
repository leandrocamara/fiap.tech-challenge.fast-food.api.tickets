using Adapters.Gateways;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Entities.SeedWork;

namespace External.Persistence.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IAggregatedRoot
    {
        private readonly DynamoDBContext _context;

        protected BaseRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient);
        }

        public async Task Add(T entity) => await _context.SaveAsync<T>(entity);    
        public async Task Delete(T entity) => await _context.DeleteAsync<T>(entity);        
        public async Task<T?> GetById(Guid id) => await _context.LoadAsync<T?>(id);        
        public async Task Update(T entity) => await _context.SaveAsync<T>(entity);               
    }
}
