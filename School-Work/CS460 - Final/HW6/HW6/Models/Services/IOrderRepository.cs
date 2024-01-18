using HW6.Models;

namespace HW6.DAL.Abstract;

public interface IOrderRepository : IRepository<Order>
{
    List<Order> GetAllAscending();
    bool CheckStatus(int id);
}