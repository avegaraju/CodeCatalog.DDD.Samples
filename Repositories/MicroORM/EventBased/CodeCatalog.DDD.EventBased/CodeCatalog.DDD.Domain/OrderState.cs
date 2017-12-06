using System;
using System.Collections.Generic;

namespace CodeCatalog.DDD.Domain
{
    public class OrderState
    {
        public Guid OrderId { get; set; }
        public CustomerState Customer { get; set; }
        public IEnumerable<OrderLineState> OrderLines { get; set; }
        public decimal OrderAmount { get; set; }
        public bool PaymentProcessed { get; set; }
        public Guid PaymentTransactionReference { get; set; }
    }
}
