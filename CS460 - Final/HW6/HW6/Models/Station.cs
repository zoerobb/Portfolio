using System;
using System.Collections.Generic;

namespace HW6.Models;

public partial class Station
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MenuItem> MenuItems { get; } = new List<MenuItem>();
}
