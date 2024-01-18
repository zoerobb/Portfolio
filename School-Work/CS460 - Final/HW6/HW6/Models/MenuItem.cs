using System;
using System.Collections.Generic;

namespace HW6.Models;

public partial class MenuItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public int? StationId { get; set; }

    public virtual ICollection<OrderedItem> OrderedItems { get; } = new List<OrderedItem>();

    public virtual Station? Station { get; set; }
}
