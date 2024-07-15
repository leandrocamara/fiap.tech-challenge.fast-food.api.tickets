using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.Persistence.Tables
{
    internal class TicketDefinitions
    {
        public static readonly string TABLE_NAME = "tickets_table";
        public static readonly string PARTITION_KEY = "pk";
        public static readonly string SORT_KEY = "sk";
    }
}
