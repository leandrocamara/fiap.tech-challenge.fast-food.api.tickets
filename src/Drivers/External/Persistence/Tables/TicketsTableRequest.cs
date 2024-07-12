using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace External.Persistence.Tables
{
    internal class TicketsTableRequest: CreateTableRequest
    {
        public TicketsTableRequest()
        {
            TableName = TicketDefinitions.TABLE_NAME;
            AttributeDefinitions = new List<AttributeDefinition>
            {
                new AttributeDefinition(TicketDefinitions.PARTITION_KEY, ScalarAttributeType.S),
                new AttributeDefinition(TicketDefinitions.SORT_KEY  , ScalarAttributeType.S),
                
            };
            KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement(TicketDefinitions.PARTITION_KEY, KeyType.HASH),
                new KeySchemaElement(TicketDefinitions.SORT_KEY, KeyType.RANGE)
            };
            ProvisionedThroughput = new ProvisionedThroughput(1, 1);
        }
    }
}
