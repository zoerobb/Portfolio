using HW6.Models;

namespace HW6.DAL.Abstract;

public interface IMenuItemRepository : IRepository<MenuItem>
{
    decimal GetPrice(int id);
}