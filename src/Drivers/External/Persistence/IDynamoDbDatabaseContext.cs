using System.Threading.Tasks;

namespace External.Persistence
{
    internal interface IDynamoDbDatabaseContext
    {
        Task ConfigureAsync();
        Task<bool> TableExist(string tableName);
    }
}