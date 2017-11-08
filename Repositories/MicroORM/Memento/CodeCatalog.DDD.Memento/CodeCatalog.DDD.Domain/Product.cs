using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain
{
    public partial class Product
    {
        public ProductId ProductId { get; }
        public double Discount { get; }
        public double Price { get; }

        private Product(ProductId productId, 
                        double discount, 
                        double price)
        {
            ProductId = productId;
            Discount = discount;
            Price = price;
        }
    }
}
