using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFirstApp.Models;
using MyFirstApp.Services;

namespace MyFirstApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly CartService _cartService;
        private ApplicationDbContext _context;

        public OrdersController(CartService cartService, ApplicationDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        [Authorize()]
        public IActionResult Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _cartService.GetCart();

            if(cart == null)
            {
                return NotFound();
            }

            var order = new Order {
                UserId = userId,
                Total = cart.CartItems.Sum(CartItem => (decimal)(CartItem.Quantity * CartItem.Service.MSRP)),
                 OrderItems = new List<OrderItem>()
            };

            foreach(var cartItem in cart.CartItems )
            {
                order.OrderItems.Add(new OrderItem {
                    OrderId = order.Id,
                    ServiceName = cartItem.Service.ServiceName,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Service.MSRP
                });
            }

            return View("Details", order);
        }
    }
}