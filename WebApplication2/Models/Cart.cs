using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int? CartQuantity { get; set; }

    public int CusId { get; set; }

    public int? ProductId { get; set; }

    public virtual Customer Cus { get; set; } = null!;

    public virtual Product? Product { get; set; }
}
