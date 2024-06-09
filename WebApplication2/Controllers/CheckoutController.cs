using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication2.Models;
using WebApplication2.Models.CheckoutView;

namespace WebApplication2.Controllers
{
    public class CheckoutController : Controller
    {
        OganiContext db = new OganiContext();
        public IActionResult ViewCheckout(int cusId)
        {
            var customer = db.Customers.Find(cusId);
            var cartItems = db.Carts.Include(c => c.Product).Where(c => c.CusId == cusId).ToList();
            decimal? totalPrice = cartItems.Sum(item => item.Product.ProductPrice * item.CartQuantity);
            var checkoutView = new CheckoutView
            {
                Customer = customer,
                CartItems = cartItems,
                TotalPrice = totalPrice,
            };
            return View(checkoutView);
        }
        [HttpPost]
        public IActionResult PlaceOrder(int cusId, string CustomerName, string CustomerAddress, string CustomerPhone, string paymentMethod, string shippingMethod)
        {
            var cartItems = db.Carts.Include(c => c.Product).Where(c => c.CusId == cusId).ToList();
            foreach (var item in cartItems)
            {
                item.Product.ProductQuantity -= item.CartQuantity;
                item.Product.ProductSellquantity += item.CartQuantity;
            }
            db.SaveChanges();

            var shipping = new Shipment
            {
                ShipAddress = CustomerAddress,
                ShipState = "Preparing",
                ShipCode = shippingMethod, // SUA DB CODE -> METHOD
                CusId = cusId
            };
            db.Shipments.Add(shipping);
            db.SaveChanges();

            var payment = new Payment
            {
                PaymentMethod = paymentMethod,
                PaymentAmount = (shippingMethod == "FastExpress" ? 30000 : 15000),
                CusId = cusId
            };
            db.Payments.Add(payment);
            db.SaveChanges();

            var order = new Order
            {
                OrderTotalprice = (
                    shippingMethod == "FastExpress" ? 
                        cartItems.Sum(item => item.Product.ProductPrice * item.CartQuantity) + 30000 :
                        cartItems.Sum(item => item.Product.ProductPrice * item.CartQuantity) + 15000
                ),

                CusId = cusId,
                PaymentId = payment.PaymentId,
                ShipId = shipping.ShipId,
            };
            db.Orders.Add(order);
            db.SaveChanges();

            foreach(var cartItem in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderItemQuantity = cartItem.CartQuantity,
                    OrderItemPrice = cartItem.Product.ProductPrice,
                    ProductId = cartItem.Product.ProductId,
                    OrderId = order.OrderId
                };
                db.OrderItems.Add(orderItem);
            }
            db.Carts.RemoveRange(cartItems);
            db.SaveChanges();
            return RedirectToAction("ViewCart", "Cart", new { cusId = cusId });
        }
    }
}
