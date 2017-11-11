using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain.UseCase
{
    public class ProductRequest
    {
        public ProductId ProductId { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        
    }
}
