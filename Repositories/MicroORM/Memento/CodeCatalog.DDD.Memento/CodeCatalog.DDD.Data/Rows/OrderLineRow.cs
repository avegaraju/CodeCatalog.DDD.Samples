namespace CodeCatalog.DDD.Data.Rows
{
    internal class OrderLineRow
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

    }
}
