using System.Linq;
using Microsoft.EntityFrameworkCore;
using HW6.DAL.Abstract;
using HW6.Models;

namespace HW6.DAL.Concrete
{
    public class OrderedItemRepository : Repository<OrderedItem>, IOrderedItemRepository
    {
        private DbSet<OrderedItem> _orderedItems;
        private readonly DbContext _context;

        public OrderedItemRepository(CoffeeShopDbContext context) : base(context)
        {
            _orderedItems = context.OrderedItems;
            _context = context;
        }

        public bool CheckStatus(int id)
        {
            var orderedItem = _orderedItems.Where(o => o.Id == id).FirstOrDefault() ?? throw new Exception($"Ordered item with ID {id} not found");
            return orderedItem.Complete;
        }
    }
}