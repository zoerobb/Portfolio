using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace HW6.Models.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? StoreId { get; set; }
        public int? DeliveryId { get; set; }
        public string DeliveryName { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Complete { get; set; }
        public DateTime TimeArrived { get; set; }
    }

}

namespace HW6.ExtensionMethods
{
    public static class OrderExtensions
    {
        public static HW6.Models.DTO.OrderDTO ToOrderDTO(this HW6.Models.Order order)
        {
            return new HW6.Models.DTO.OrderDTO
            {
                Id = order.Id,
                Name = order.Name,
                DeliveryName = order.Delivery.Name,
                StoreId = order.StoreId,
                TotalPrice = order.TotalPrice,
                Complete = order.Complete,
                TimeArrived = order.TimeArrived
            };
        }

        public static HW6.Models.Order ToOrder(this HW6.Models.DTO.OrderDTO order)
        {
            return new HW6.Models.Order
            {
                Id = order.Id,
                Name = order.Name,
                DeliveryId = order.DeliveryId,
                StoreId = order.StoreId,
                TotalPrice = order.TotalPrice,
                Complete = order.Complete,
                TimeArrived = order.TimeArrived
            };
        }
    }
}