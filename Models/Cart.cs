namespace MyFirstApp.Models
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}