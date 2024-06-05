using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? OrderTotalprice { get; set; }

    public DateOnly? OrderAdd { get; set; }

    public int? CusId { get; set; }

    public int? PaymentId { get; set; }

    public int? ShipId { get; set; }

    public virtual Customer? Cus { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Payment? Payment { get; set; }

    public virtual Shipment? Ship { get; set; }
}
