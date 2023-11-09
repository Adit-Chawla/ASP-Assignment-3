using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MyFirstApp.Models
{
    public class CartsController : Controller
    {
        private readonly string _cartSessionKey;
        private readonly ApplicationDbContext _context;

        public CartsController(ApplicationDbContext context)
        {
            _cartSessionKey = "Cart";
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cart = GetCart();
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
            var cart = GetCart();

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

            SaveCart(cart);

            return RedirectToAction("Index");
        }

        private Cart? GetCart()
        {
            var cartJson = HttpContext.Session.GetString(_cartSessionKey);
            return cartJson == null ? new Cart() : JsonConvert.DeserializeObject<Cart>(cartJson);
        }

        private void SaveCart(Cart cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(_cartSessionKey, cartJson);
        }
    }
}
