using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.Persistence.Tables
{
    internal class TicketDefinitions
    {
        public static readonly string TABLE_NAME = "FastFood.Tickets";
        public static readonly string PARTITION_KEY = "PartitionKey";
        public static readonly string SORT_KEY = "SortKey";
    }
}
