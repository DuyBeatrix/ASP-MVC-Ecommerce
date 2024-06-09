namespace WebApplication2.Models.CheckoutView
{
    public class CheckoutView
    {
        public Customer Customer { get; set; }
        public IEnumerable<Cart> CartItems { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalPrice1 { get; set; }
    }
}
