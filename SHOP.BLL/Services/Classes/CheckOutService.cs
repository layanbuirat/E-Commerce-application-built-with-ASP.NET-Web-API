using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using SHOP.BLL.Services.Interfaces;
using SHOP.DAL.DTO.Requests;
using SHOP.DAL.DTO.Responses;
using SHOP.DAL.Models;
using SHOP.DAL.Repositories.Interfaces;
using Stripe.Checkout;

namespace SHOP.BLL.Services.Classes
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _email;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        public CheckOutService(ICartRepository cartRepository,
            IOrderRepository orderRepository, IEmailSender email,
            IOrderItemRepository orderItemRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _email = email;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
            var order = await _orderRepository.GetUserByOrderAsync(orderId);
            var subject = "";
            var body = "";
            if (order.PaymentMethod == PaymentMethod.Visa)
            {

                order.Status = OrderStatus.Approved;

                var carts = await _cartRepository.GetUserCartAsync(order.UserId);
                var orderItems = new List<OrderItem>();
                var productUpdate = new List<(int ProductId, int quantity)>();
                foreach (var cartItem in carts)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = cartItem.ProductId,
                        TotalPrice = cartItem.Product.Price * cartItem.Count,
                        Count = cartItem.Count,
                        Price = cartItem.Product.Price
                    };
                    orderItems.Add(orderItem);
                    productUpdate.Add((cartItem.ProductId, cartItem.Count));

                }
                await _orderItemRepository.AddRangeAsync(orderItems);
                await _cartRepository.ClearCartAsync(order.UserId);
                await _productRepository.DecreaseQuantityAsync(productUpdate);

                subject = "Payment Successful";
                body = "<h1>Thank you for your payment</h1>" +
                    $"<p>Your payment for order {orderId}</p>" +
                    $"<p>Total Amount : ${order.TotalAmount}</p>";
            }
            else if (order.PaymentMethod == PaymentMethod.Cash)
            {
                subject = "Order Placed";
                body = "<h1>Thank you for your order</h1>" +
                    $"<p>Your order {orderId} has been placed successfully.</p>" +
                    $"<p>Total Amount : ${order.TotalAmount}</p>";
            }

            await _email.SendEmailAsync(order.User.Email, subject, body);
            return true;
        }

        public async Task<CheckOutResponse> ProcsessPaymentAsync(CheckOutRequest request, string UserId, HttpRequest httpRequest)
        {
            var cartItems = await _cartRepository.GetUserCartAsync(UserId);

            if (!cartItems.Any())
            {
                return new CheckOutResponse
                {
                    Success = false,
                    Message = "Cart is empty."
                };
            }

            Order order = new Order
            {
                UserId = UserId,
                PaymentMethod = request.PaymentMethod,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                TotalAmount = cartItems.Sum(c => c.Product.Price * c.Count)
            };

            await _orderRepository.AddAsync(order);

            if (request.PaymentMethod == PaymentMethod.Cash)
            {
                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Cash."
                };
            }

            if (request.PaymentMethod == PaymentMethod.Visa)
            {

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {

                    },


                    Mode = "payment",
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/Customer/CheckOuts/Success/{order.Id}",
                    CancelUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/checkout/cancel",
                };
                foreach (var item in cartItems)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                Description = item.Product.Description,
                            },
                            UnitAmount = (long)item.Product.Price,
                        },
                        Quantity = item.Count,
                    });
                }
                var service = new SessionService();
                var session = service.Create(options);

                order.PaymentId = session.Id;

                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Payment processed successfully.",
                    PaymentId = session.Id,
                    Url = session.Url
                };
            }

            return new CheckOutResponse
            {
                Success = false,
                Message = "Invalid payment method."
            };
        }
    }
}
