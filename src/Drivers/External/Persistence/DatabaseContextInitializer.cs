using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace External.Persistence
{
    internal class DatabaseContextInitializer
    {
        private readonly IDynamoDbDatabaseContext _databaseContext;

        public DatabaseContextInitializer(IDynamoDbDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext 
            ?? throw new ArgumentNullException(nameof(databaseContext));
            Initialize().Wait();
        }

        private async Task Initialize() 
            => await _databaseContext.ConfigureAsync();
    }
}

