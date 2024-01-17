using System.Linq;
using Microsoft.EntityFrameworkCore;
using HW6.DAL.Abstract;
using HW6.Models;

namespace HW6.DAL.Concrete
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private DbSet<Order> _order;
        private readonly DbContext _context;

        public OrderRepository(CoffeeShopDbContext context) : base(context)
        {
            _context = context;
            _order = context.Orders;
        }

        public bool CheckStatus(int id)
        {
            var order =  _order.Where(o => o.Id == id).FirstOrDefault() ?? throw new Exception($"Order with ID {id} not found");

            return order.Complete;
        }

        public List<Order> GetAllAscending()
        {
            return _order.Where(o => !o.Complete).OrderBy(o => o.TimeArrived).ToList();
        }
    }
}