using System;

using CodeCatalog.DDD.Data;
using CodeCatalog.DDD.Domain.Infrastructure;
using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCases;
using CodeCatalog.DDD.Infrastucture;

using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CodeCatalog.DDD.Client
{
    public class Program
    {
        private static Container _container;

        static Program()
        {
            ConfigureDependencies();
        }

        private static void ConfigureDependencies()
        {
            _container = new Container
                         {
                             Options =
                             {
                                 DefaultScopedLifestyle = new AsyncScopedLifestyle(),
                             }
                         };

            _container.Register<IOrderRepository>(()=> new OrderRepository("DDDRepositorySampleDb.sqlite"));
            _container.Register<IPaymentService, PaymentService>();
        }

        public static void Main(string[] args)
        {
            var orderRequest = CreateShoppingCart()
                    .AddProduct(ProductCatalog.Nexus6p,
                                discount: 10.2m,
                                price: 445,
                                quantity: 1)
                    .AddProduct(ProductCatalog.Nexus5x,
                                discount: 8.2m,
                                price: 329,
                                quantity: 1)
                    .ForCustomer((CustomerId)1)
                    .BuildOrderRequest();

            var newOrderId
                    = new CreateOrder(_container.GetInstance<IOrderRepository>())
                            .Create(orderRequest);

            var amountToPay = new CheckOutOrder(_container.GetInstance<IOrderRepository>())
                    .Checkout(newOrderId);

            var transactionResult = new OrderPayment(_container.GetInstance<IOrderRepository>(),
                                                     _container.GetInstance<IPaymentService>())
                    .Pay(newOrderId, amountToPay);

            if (transactionResult.PaymentStatus == PaymentStatus.Succeeded
                && transactionResult.OrderTransactionStatus == OrderTransactionStatus.Succeeded)
            {
                Console.WriteLine("Your order has been placed.");
            }
            else
            {
                Console.WriteLine("Order could not be placed. Please try again.");
            }

            Console.ReadLine();
        }

        private static ShoppingCart CreateShoppingCart()
        {
            return new ShoppingCart();
        }
    }
}
