using System;
using System.Collections.Generic;

namespace HW6.Models;

public partial class OrderedItem
{
    public int Id { get; set; }

    public int? MenuItemId { get; set; }

    public int OrderId { get; set; }

    public int Quantity { get; set; }

    public bool Complete { get; set; }

    public virtual MenuItem? MenuItem { get; set; }

    public virtual Order? Order { get; set; }
}
