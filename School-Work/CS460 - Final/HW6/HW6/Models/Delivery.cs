using System;
using System.Collections.Generic;

namespace HW6.Models;

public partial class Delivery
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
