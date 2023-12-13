using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstApp.Services;


namespace MyFirstApp.Models
{
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;

        public CartsController(CartService cartService, ApplicationDbContext context)
        {
            _context = context;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var cart = _cartService.GetCart();
            cart ??= new Cart();

            if(cart == null){
                return NotFound();
            }

            if(cart.CartItems.Count > 0)
            {
                foreach(var cartItem in cart.CartItems)
                {
                    var service = await _context.Services
                        .Include(p => p.Department)
                        .FirstOrDefaultAsync(p => p.Id == cartItem.ServiceId);

                    if(service != null)
                    {
                        cartItem.Service = service;
                    }
                }
            }

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int serviceId, int quantity)
        {
            var cart = _cartService.GetCart();

            if(cart==null){
                return NotFound();
            }

            var cartItem = cart.CartItems.Find(cartItem => cartItem.ServiceId == serviceId);
            
            if(cartItem != null && cartItem.Service != null)
            {
                cartItem.Quantity += quantity;
            }
            else{
                var service = await _context.Services
                    .FirstOrDefaultAsync(p => p.Id == serviceId);

                if(service == null)
                {
                    return NotFound();
                }

                cartItem = new CartItem {ServiceId=serviceId, Quantity=quantity, Service=service};
                cart.CartItems.Add(cartItem);
            }

            _cartService.SaveCart(cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int serviceId)
        {
            var cart = _cartService.GetCart();

            if(cart == null)
            {
                return NotFound();
            }

            var cartItem = cart.CartItems.Find(cartItem => cartItem.ServiceId == serviceId);

            if(cartItem!=null)
            {
                cart.CartItems.Remove(cartItem);
                _cartService.SaveCart(cart);
            }

            return RedirectToAction("Index");
        }
    }
}
