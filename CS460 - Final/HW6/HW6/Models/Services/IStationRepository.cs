using HW6.Models;

namespace HW6.DAL.Abstract;

public interface IStationRepository : IRepository<Station>
{
    string GetStationName(int id);
}