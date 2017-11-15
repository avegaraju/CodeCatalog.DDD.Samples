namespace CodeCatalog.DDD.Domain.UseCases
{
    public class CreateOrder
    {
        public void Create(OrderRequest request)
        {
            var order = Order.OrderFactory.CreateFrom(request);
        }
    }
}