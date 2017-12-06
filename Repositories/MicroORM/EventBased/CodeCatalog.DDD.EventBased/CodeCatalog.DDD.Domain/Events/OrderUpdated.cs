using System;

namespace CodeCatalog.DDD.Domain.Events
{
    public class OrderUpdated: IEvent
    {
        public Guid OrderId { get; }
        public decimal OrderAmount {get;}
        public Guid PaymentTransactionReference { get; }

        public OrderUpdated(Guid orderId,
                            decimal orderAmount,
                            Guid paymentTransactionReference)
        {
            OrderId = orderId;
            OrderAmount = orderAmount;
            PaymentTransactionReference = paymentTransactionReference;
        }
    }
}
