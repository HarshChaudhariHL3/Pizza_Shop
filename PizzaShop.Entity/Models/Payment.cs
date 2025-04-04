﻿using System;
using System.Collections.Generic;

namespace PizzaShop.Entity.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentStatus { get; set; }

    public float? Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
