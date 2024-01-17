  
  using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace HW6.Models.DTO
{
    public class OrderedItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public bool Complete { get; set; }
        public string Name { get; set; }
    }
}

namespace HW6.ExtensionMethods
{
    public static class OrderedItemExtensions
    {
        public static HW6.Models.DTO.OrderedItemDTO ToOrderedItemDTO(this HW6.Models.OrderedItem orderedItem)
        {
            return new HW6.Models.DTO.OrderedItemDTO
            {
                Id = orderedItem.Id,
                Quantity = orderedItem.Quantity,
                Complete = orderedItem.Complete,
                Name = orderedItem.MenuItem.Name
            };
        }

        public static HW6.Models.OrderedItem ToOrderedItem(this HW6.Models.DTO.OrderedItemDTO orderedItem)
        {
            return new HW6.Models.OrderedItem
            {
                Id = orderedItem.Id,
                Quantity = orderedItem.Quantity,
                Complete = orderedItem.Complete
            };
        }
    }
}