using System;
using System.Collections.Generic;

namespace PizzaShop.Entity.Models;

public partial class ModifierGroup
{
    public int ModifierGroupId { get; set; }

    public string ModifierName { get; set; } = null!;

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<MappingItemModifier> MappingItemModifiers { get; set; } = new List<MappingItemModifier>();
}
