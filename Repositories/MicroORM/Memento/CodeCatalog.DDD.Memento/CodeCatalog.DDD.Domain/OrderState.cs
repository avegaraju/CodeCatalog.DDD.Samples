using System;
using System.Collections.Generic;

namespace CodeCatalog.DDD.Domain
{
    public class OrderState
    {
        public Guid OrderId { get; set; }
        public CustomerState Customer { get; set; }
        public IEnumerable<OrderLineState> OrderLines { get; set; }
    }
}
