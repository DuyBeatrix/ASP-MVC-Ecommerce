using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? PaymentMethod { get; set; }

    public decimal? PaymentAmount { get; set; }

    public int? CusId { get; set; }

    public virtual Customer? Cus { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
