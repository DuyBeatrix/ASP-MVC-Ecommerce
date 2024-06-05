using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int? OrderItemQuantity { get; set; }

    public decimal? OrderItemPrice { get; set; }

    public DateOnly? OrderItemAdd { get; set; }

    public int ProductId { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product Product { get; set; } = null!;
}
