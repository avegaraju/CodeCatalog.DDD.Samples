using System;

using CodeCatalog.DDD.Domain.Infrastructure;
using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Infrastucture
{
    /// <summary>
    /// This is a dummy implementation of Payment Service.
    /// </summary>
    public class PaymentService: IPaymentService
    {
        public PaymentReference Pay(decimal amount, Guid orderId)
        {
            //Payment is successful always!

            return new PaymentReference()
                   {
                       TransactionId = Guid.NewGuid()
                   };
        }
    }
}
