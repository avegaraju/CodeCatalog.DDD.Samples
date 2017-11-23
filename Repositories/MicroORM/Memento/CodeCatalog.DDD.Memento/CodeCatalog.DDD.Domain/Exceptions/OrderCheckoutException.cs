using System;

namespace CodeCatalog.DDD.Domain.Exceptions
{
    public class OrderCheckoutException: Exception
    {
        public OrderCheckoutException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}
