using System;
using System.Data.SqlClient;

using Dapper;

namespace CodeCatalog.DDD.Data.Test.Integration.Helpers
{
    internal class OrderHelper: HelperBase
    {
        public OrderRow Get(Guid orderId)
        {
            using (Connection)
            {
                return Connection
                        .QueryFirst<OrderRow>(sql: $@"select * from Orders where id = ""{orderId.ToString()}"";");
            }
        }
    }
}
