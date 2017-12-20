using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain
{
    public class OrderLineState
    {
        public OrderLineId? OrderLineId { get; set; }
        public ProductId ProductId { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public uint Quantity { get; set; }
    }
}
