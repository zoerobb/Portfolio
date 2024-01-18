using System;
using System.Collections.Generic;

namespace HW6.Models;

public partial class Order
{
    public int Id { get; set; }

    public string Name { get; set; } 

    public int? DeliveryId { get; set; }

    public int? StoreId { get; set; }

    public decimal TotalPrice { get; set; }

    public bool Complete { get; set; }

    public DateTime TimeArrived { get; set; }

    public virtual Delivery? Delivery { get; set; }

    public virtual ICollection<OrderedItem> OrderedItems { get; } = new List<OrderedItem>();

    public virtual Store? Store { get; set; }
}
