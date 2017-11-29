using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain
{
    public partial class OrderLine
    {
        internal ProductId ProductId { get; }
        internal decimal Discount { get; }
        internal decimal Price { get; }
        internal uint Quantity { get; }

        private OrderLine(ProductId productId,
                          decimal discount,
                          decimal price,
                          uint quantity)
        {
            ProductId = productId;
            Discount = discount;
            Price = price;
            Quantity = quantity;
        }

        public OrderLineState GetState()
        {
            return new OrderLineState()
                   {
                       ProductId = this.ProductId,
                       Discount = this.Discount,
                       Price = this.Price,
                       Quantity = this.Quantity
                   };
        }
    }
}