namespace CodeCatalog.DDD.Data.Rows
{
    internal class OrderRow
    {
        public string Id { get; set; }
        public long CustomerId { get; set; }
        public char PaymentProcessed { get; set; }
    }
}
