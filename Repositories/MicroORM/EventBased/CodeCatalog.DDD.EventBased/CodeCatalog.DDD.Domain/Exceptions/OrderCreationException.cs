using System;

namespace CodeCatalog.DDD.Domain.Exceptions
{
    public class OrderCreationException: Exception
    {
        public OrderCreationException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}
