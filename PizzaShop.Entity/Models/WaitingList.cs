using System;
using System.Collections.Generic;

namespace PizzaShop.Entity.Models;

public partial class WaitingList
{
    public int WaitingId { get; set; }

    public int? CustomerId { get; set; }

    public int? TokenId { get; set; }

    public int? TableId { get; set; }

    public string? WaitingStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual TableDetail? Table { get; set; }
}
