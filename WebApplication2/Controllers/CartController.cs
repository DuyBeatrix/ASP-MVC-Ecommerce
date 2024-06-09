using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Models.Authentication;
using System.Security.Claims;
using WebApplication2.Models.CartTotalPrice;
namespace WebApplication2.Controllers
{
    public class CartController : Controller
    {
        OganiContext db = new OganiContext();
        [Authentication]
        public IActionResult ViewCart(int cusId)
        {
            var cartItems = db.Carts.Include(c => c.Product).Where(c => c.CusId == cusId).ToList();

            decimal? totalPrice = cartItems.Sum(item => item.Product.ProductPrice * item.CartQuantity);

            var cartTotalPrice = new CartTotalPrice
            {
                CartItems = cartItems,
                TotalPrice = totalPrice
            };

            return View(cartTotalPrice);
        }

        [HttpPost]
        [Authentication]
        public IActionResult AddToCart(int productid, int quantity, int cusID) 
        {
            var cartItem = db.Carts.FirstOrDefault(x => x.CusId == cusID && x.ProductId == productid);  
            if(cartItem == null)
            {
                cartItem = new Cart
                {
                    CusId = cusID,
                    ProductId = productid,
                    CartQuantity = quantity,
                    
                };
                db.Carts.Add(cartItem); 
            }
            else
            {
                cartItem.CartQuantity += quantity;
            }
            db.SaveChanges();
            return RedirectToAction("ViewCart", "Cart", new {cusId = cusID});
        }

        [HttpPost]
        public IActionResult UpdateCart(int cartId, int quantity)
        {
            var cartItem = db.Carts.FirstOrDefault(c => c.CartId == cartId);
            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    cartItem.CartQuantity = quantity;
                }
                else
                {
                    db.Carts.Remove(cartItem);
                }
                db.SaveChanges();
            }
            return RedirectToAction("ViewCart", "Cart", new {cusId = cartItem.CusId});
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int cartId)
        {
            var cartItem = db.Carts.FirstOrDefault(c => c.CartId == cartId);
            if (cartItem != null)
            {
                db.Carts.Remove(cartItem);
                db.SaveChanges();
            }
            return RedirectToAction("ViewCart", "Cart", new { cusId = cartItem.CusId });
        }
    }
}
