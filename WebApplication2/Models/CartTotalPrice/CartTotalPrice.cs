namespace WebApplication2.Models.CartTotalPrice
{
    public class CartTotalPrice
    {
        public IEnumerable<Cart> CartItems { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
