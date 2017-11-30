namespace CodeCatalog.DDD.Data.Rows
{
    internal class OrderRow
    {
        public string Id { get; set; }
        public int CustomerId { get; set; }
        public char PaymentProcessed { get; set; }
    }
}
