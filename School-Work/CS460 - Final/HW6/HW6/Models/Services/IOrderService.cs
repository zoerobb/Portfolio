using HW6.Models;
using HW6.Models.DTO;

namespace HW6.Services
{
    public interface IOrderService
    {
        Task RunOrderGeneratorScript(ReceivedOrder data);
        List<Order> GetOrders();
        List<OrderedItem> GetOrderedItems(int id);
        List<OrderedItem> GetOrderedItemsForAStation(int id);
        void CompleteOrderItem(int id);
        void UpdateOrderStatus(int id);

        void UpdateAllOrders();
    }
}