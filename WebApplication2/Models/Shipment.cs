using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Shipment
{
    public int ShipId { get; set; }

    public DateTime? ShipDate { get; set; }

    public string? ShipAddress { get; set; }

    public string? ShipState { get; set; }

    public string? ShipCode { get; set; }

    public int? CusId { get; set; }

    public virtual Customer? Cus { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
