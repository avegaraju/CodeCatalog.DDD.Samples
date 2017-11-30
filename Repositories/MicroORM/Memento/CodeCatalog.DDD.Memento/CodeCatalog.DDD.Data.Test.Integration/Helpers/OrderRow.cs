using System;

namespace CodeCatalog.DDD.Data.Test.Integration.Helpers
{
    internal class OrderRow
    {
        public string Id { get; set; }
        public long CustomerId { get; set; }
        public char PaymentProcessed { get; set; }
        public decimal OrderAmount { get; set; }
        public string PaymentTransactionReference { get; set; }
    }
}
