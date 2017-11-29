using System;

using CodeCatalog.DDD.Domain.Infrastructure;
using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Infrastucture
{
    public class PaymentService: IPaymentService
    {
        public PaymentReference Pay(decimal amount, Guid orderId)
        {
            return new PaymentReference()
                   {
                       TransactionId = Guid.NewGuid()
                   };
        }
    }
}
