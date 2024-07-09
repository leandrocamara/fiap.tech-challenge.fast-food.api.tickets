using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using External.Persistence.Tables;

namespace External.Persistence
{
    internal class TicketDynamoDbDatabaseContext : IDynamoDbDatabaseContext
    {
        private readonly IAmazonDynamoDB _dynamoDb;

        public TicketDynamoDbDatabaseContext(IAmazonDynamoDB dynamoDb) 
            => _dynamoDb = dynamoDb 
            ?? throw new ArgumentNullException(nameof(dynamoDb));

        public async Task ConfigureAsync()
        {
            var request = new TicketsTableRequest();
            await CreateIfNotExist(request, TicketDefinitions.TABLE_NAME);
        }

        public async Task<bool> TableExist(string tableName)
        {
            var tables = await _dynamoDb.ListTablesAsync();
            var existTable = tables.TableNames.Contains(tableName);
            return existTable;
        }

        private async Task CreateIfNotExist(CreateTableRequest request, string tableName)
        {
            if (await TableExist(tableName)) { return; }

            await _dynamoDb.CreateTableAsync(request);
            
        }
    }
}
