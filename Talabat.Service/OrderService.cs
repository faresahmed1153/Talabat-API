using Talabat.Core;
using Talabat.Core.Models;
using Talabat.Core.Models.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specifications.OrderSpecifications;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        //private readonly IGenericRepository<Product> _productsRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodsRepo;
        //private readonly IGenericRepository<Order> _ordersRepo;

        public OrderService(ICartRepository cartRepository,
            //IGenericRepository<Product> productsRepo,
            //IGenericRepository<DeliveryMethod> deliveryMethodsRepo,
            //IGenericRepository<Order> ordersRepo
            IUnitOfWork unitOfWork,
            IPaymentService paymentService

            )
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            //_productsRepo = productsRepo;
            //_deliveryMethodsRepo = deliveryMethodsRepo;
            //_ordersRepo = ordersRepo;
        }

        public async Task<Order?> CreateOrderAsync(string buyerEmail, string cartId, int deliveryMethodId, Address shippingAddress)
        {

            var cart = await _cartRepository.GetCartAsync(cartId);

            var orderItems = new List<OrderItem>();

            if (cart?.Items.Count > 0)
            {
                var productsRepo = _unitOfWork.Repository<Product>();

                foreach (var item in cart.Items)
                {
                    if (productsRepo is not null)
                    {
                        var product = await productsRepo.GetByIdAsync(item.Id);


                        if (product is not null)
                        {
                            var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);

                            var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);

                            orderItems.Add(orderItem);
                        }
                    }

                }

            }



            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            DeliveryMethod deliveryMethod = new DeliveryMethod();

            var deliveryMethodsRepo = _unitOfWork.Repository<DeliveryMethod>();

            if (deliveryMethodsRepo is not null)
                deliveryMethod = await deliveryMethodsRepo.GetByIdAsync(deliveryMethodId);

            var spec = new OrderWithPaymentIntentIdSpecification(cart.PaymentIntentId);

            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);

            if (existingOrder is not null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(cart.Id);
            }

            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subtotal, cart.PaymentIntentId);

            var ordersRepo = _unitOfWork.Repository<Order>();

            if (ordersRepo is not null)
            {

                await ordersRepo.Add(order);

                var result = await _unitOfWork.Complete();

                if (result > 0)
                    return order;
            }

            return null;

        }



        public async Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {

            var spec = new OrderSpecifications(buyerEmail, orderId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);



            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {

            var spec = new OrderSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

            return deliveryMethod;
        }
    }
}
