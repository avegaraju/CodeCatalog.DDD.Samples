using System;
using System.Collections.Generic;

using Dapper;

namespace CodeCatalog.DDD.Data.Test.Integration.Helpers
{
    internal class OrderLineHelper: HelperBase
    {
        public IEnumerable<OrderLineRow> Get(Guid orderId)
        {
            return Connection.Query<OrderLineRow>(sql: 
                $@"select * from OrderLines where orderId = ""{orderId.ToSqliteGuid()};""");
        }
    }
}
