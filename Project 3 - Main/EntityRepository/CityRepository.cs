using Project_3___Press_Project.Entity;
using System;
using System.Linq;

namespace Project_3___Press_Project.Repository
{
    public class CityRepository : RepositoryBase<City>
    {
        public override bool Exists(City entity)
        {
            throw new System.NotImplementedException();
        }

        public City FindByName(String name)
        {
            City city = _context.Cities.SingleOrDefault(c => c.Name == name);
            return city;
        }
    }
}
