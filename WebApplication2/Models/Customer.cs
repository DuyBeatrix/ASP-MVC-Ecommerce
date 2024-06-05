using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Customer
{
    public int CusId { get; set; }

    public string? CusName { get; set; }

    public string? CusEmail { get; set; }

    public string? CusPassword { get; set; }

    public string? CusAddress { get; set; }

    public string? CusPhone { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
