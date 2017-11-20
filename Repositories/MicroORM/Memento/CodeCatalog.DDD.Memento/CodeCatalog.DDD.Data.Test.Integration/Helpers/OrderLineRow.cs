using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Data.Test.Integration.Helpers
{
    internal class OrderLineRow
    {
        public long Id { get; set; }
        public ProductId ProductId { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public uint Quantity { get; set; }
    }
}
