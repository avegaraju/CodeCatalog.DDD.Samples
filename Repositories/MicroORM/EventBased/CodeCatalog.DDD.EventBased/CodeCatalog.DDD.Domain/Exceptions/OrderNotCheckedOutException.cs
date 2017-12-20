using System;

namespace CodeCatalog.DDD.Domain.Exceptions
{
    public class OrderNotCheckedOutException: Exception
    {
        public OrderNotCheckedOutException(string message)
            :base(message)
        {
        }
    }
}
