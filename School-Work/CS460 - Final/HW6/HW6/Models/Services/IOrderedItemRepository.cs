using HW6.Models;

namespace HW6.DAL.Abstract;

public interface IOrderedItemRepository : IRepository<OrderedItem>
{
    bool CheckStatus(int id);
}