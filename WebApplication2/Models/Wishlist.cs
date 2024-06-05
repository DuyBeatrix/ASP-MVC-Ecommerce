using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Wishlist
{
    public int WishlistId { get; set; }

    public int CusId { get; set; }

    public int? ProductId { get; set; }

    public virtual Customer Cus { get; set; } = null!;

    public virtual Product? Product { get; set; }
}
