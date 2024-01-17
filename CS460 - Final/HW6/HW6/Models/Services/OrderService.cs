using System.Text.Json;
using HW6.Models;
using HW6.DAL.Abstract;
using HW6.Models.DTO;
using System.Diagnostics;
using Microsoft.AspNetCore.Razor.Language;

namespace HW6.Services
{
    public class ReceivedOrder
    {
        public int Store { get; set; }
        public int? Dlvy { get; set; }
        public string? Name { get; set; }
        public IList<ReceivedOrderedItem> Items { get; set; }   
    }

    public class ReceivedOrderedItem
    {
        public int Id { get; set; }
        public int Qty { get; set; }
    }
    
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IOrderedItemRepository _orderedItemRepository;
        private readonly IStationRepository _stationRepository;
        private readonly CoffeeShopDbContext _context;

        public OrderService(IOrderRepository orderRepository, IMenuItemRepository menuItemRepository, IOrderedItemRepository orderedItemRepository, IStationRepository stationRepository, CoffeeShopDbContext context)
        {
            _orderRepository = orderRepository;
            _menuItemRepository = menuItemRepository;
            _orderedItemRepository = orderedItemRepository;
            _stationRepository = stationRepository;
            _context = context;
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.GetAllAscending();
        }

        public List<OrderedItem> GetOrderedItems(int id)
        {
            var orderedItems = _orderedItemRepository.GetAll().Where(o => o.OrderId == id).ToList();

            if (!orderedItems.Any())
            {
                throw new Exception($"No ordered items found for order with ID {id}");
            }

            return orderedItems;
        }

        public List<OrderedItem> GetOrderedItemsForAStation(int id)
        {
            if(_stationRepository.FindById(id) == null)
            {
                throw new Exception($"Station with ID {id} not found");
            }

            return _orderedItemRepository.GetAll().OrderBy(o => o.Order.TimeArrived).Where(o => o.MenuItem.StationId == id && o.Complete == false).ToList();
        }

        public async Task RunOrderGeneratorScript(ReceivedOrder data)
        {
            if(data == null)
            {
                throw new Exception("No data received");
            }

            if(data.Items == null)
            {
                throw new Exception("No items received");
            }

            var order = new Order
            {
                StoreId = data.Store,
                DeliveryId = data.Dlvy,
                Name = data.Name,
                Complete = false,
                TimeArrived = DateTime.Now,
            };

            _orderRepository.AddOrUpdate(order);
            decimal totalPrice = 0;
            foreach (var itemData in data.Items)
            {
                var orderItem = new OrderedItem
                {
                    OrderId = order.Id,
                    MenuItemId = itemData.Id,
                    Quantity = itemData.Qty,
                    Complete = false
                };
                var item = _menuItemRepository.FindById(itemData.Id) ?? throw new Exception($"Item with ID {itemData.Id} not found");
                
                var itemPrice = _menuItemRepository.GetPrice(itemData.Id);

                totalPrice += itemPrice * itemData.Qty;

                _orderedItemRepository.AddOrUpdate(orderItem);
            }

            order.TotalPrice = totalPrice;
            _orderRepository.AddOrUpdate(order);

        }

        public void CompleteOrderItem(int id)
        {
            var orderedItem = _orderedItemRepository.FindById(id);

            if(orderedItem == null)
            {
                throw new Exception($"Ordered item with ID {id} not found");
            }

            if (orderedItem.Quantity > 1)
            {
                orderedItem.Quantity--;
                _orderedItemRepository.AddOrUpdate(orderedItem);
                return;
            }

            orderedItem.Complete = true;
            _orderedItemRepository.AddOrUpdate(orderedItem);
        }
    
        public void UpdateOrderStatus(int id)
        {

            List<OrderedItem> orderedItems = _orderedItemRepository.GetAll().Where(o => o.OrderId == id).ToList();

            if(!orderedItems.Any())
            {
                throw new Exception($"No ordered items found");
            }

            bool status = true;
            foreach(OrderedItem orderedItem in orderedItems)
            {
                if(!_orderedItemRepository.CheckStatus(orderedItem.Id))
                {
                    status = false;
                }
            }

            var order = _orderRepository.FindById(id);
            if(order == null)
            {
                throw new Exception($"Order with ID {id} not found");
            }

            order.Complete = status;
            _orderRepository.AddOrUpdate(order);
        }

        public void UpdateAllOrders()
        {
            List<Order> orders = _orderRepository.GetAll().ToList();

            if(!orders.Any())
            {
                throw new Exception($"No orders found");
            }

            foreach(Order order in orders)
            {
                if(order.Complete == false)
                {
                    try
                    {
                        UpdateOrderStatus(order.Id);
                    }
                    catch(Exception e)
                    {
                        throw;
                    }
                }
            }
        }
    } 
}