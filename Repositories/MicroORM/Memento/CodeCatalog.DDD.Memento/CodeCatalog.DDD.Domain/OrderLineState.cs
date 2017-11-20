using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain
{
    public class OrderLineState
    {
        public ProductId ProductId { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public uint Quantity { get; set; }
    }
}
