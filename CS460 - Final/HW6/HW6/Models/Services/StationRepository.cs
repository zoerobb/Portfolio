using System.Linq;
using Microsoft.EntityFrameworkCore;
using HW6.DAL.Abstract;
using HW6.Models;
using System.Linq.Expressions;

namespace HW6.DAL.Concrete
{
    public class StationRepository : Repository<Station>, IStationRepository
    {
        private DbSet<Station> _stations;

        public StationRepository(CoffeeShopDbContext context) : base(context)
        {
            _stations = context.Stations;
        }

        public string GetStationName(int id)
        {
            var station = _stations.Where(s => s.Id == id).FirstOrDefault();

            if(station == null)
            {
                throw new Exception($"Menu item with ID {id} not found");
            }
            
            return station.Name;
        }
    }
}