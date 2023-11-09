namespace MyFirstApp.Models
{
    public class CartItem
    {
        public int ServiceId { get; set; }

        public int Quantity {get; set; }

        public Service Service { get; set; } = new Service();
    }
}