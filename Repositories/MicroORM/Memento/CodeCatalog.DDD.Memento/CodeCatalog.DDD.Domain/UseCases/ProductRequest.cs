using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain.UseCases
{
    public class ProductRequest
    {
        public ProductId ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public uint Quantity { get; set; }
    }
}
