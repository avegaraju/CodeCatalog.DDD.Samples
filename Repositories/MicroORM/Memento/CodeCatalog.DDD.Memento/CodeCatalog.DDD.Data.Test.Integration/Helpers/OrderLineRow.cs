using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Data.Test.Integration.Helpers
{
    internal class OrderLineRow
    {
        public long Id { get; set; }
        public ProductId ProductId { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public uint Quantity { get; set; }
    }
}
