using System.Linq;
using Microsoft.EntityFrameworkCore;
using HW6.DAL.Abstract;
using HW6.Models;

namespace HW6.DAL.Concrete
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private DbSet<MenuItem> _menuItems;

        public MenuItemRepository(CoffeeShopDbContext context) : base(context)
        {
            _menuItems = context.MenuItems;
        }

        public decimal GetPrice(int id)
        {
            var menuItem = _menuItems.Where(m => m.Id == id).FirstOrDefault();

            if(menuItem == null)
            {
                throw new Exception($"Menu item with ID {id} not found");
            }
            
            return menuItem.Price;
        }
    }
}