using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain
{
    public partial class OrderLine
    {
        public ProductId ProductId { get; }
        public double Discount { get; }
        public double Price { get; }

        public OrderLine(ProductId productId, double discount, double price)
        {
            ProductId = productId;
            Discount = discount;
            Price = price;
        }
    }
}
