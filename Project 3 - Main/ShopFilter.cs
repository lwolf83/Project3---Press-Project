using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public class ShopFilter
    {

        private PressContext context;

        public ShopFilter()
        {
            context = new PressContext();
        }

        public static IEnumerable<Shop> ByCity(IEnumerable<Shop> shops, string city)
        {
            IEnumerable<Shop> returnShop = new List<Shop>();
            return returnShop;
        }

        public static IEnumerable<Shop> ByProvince(IEnumerable<Shop> shops, string province)
        {
            IEnumerable<Shop> returnShop = new List<Shop>();
            return returnShop;
        }

        public static IEnumerable<Shop> ByCountry(IEnumerable<Shop> shops, string country)
        {
            IEnumerable<Shop> returnShop = new List<Shop>();
            return returnShop;
        }

        public IEnumerable<ShopCity> GetAllShops()
        {
            var shops = from s in context.Shops
                        join a in context.Adresses on s.Adress.AddressId equals a.AddressId
                        join c in context.Cities on a.City.CityId equals c.CityId
                        orderby c.CityId
                        select new ShopCity {Shop =  s, City = c };

            return shops.ToList();
        }

        public IEnumerable<String> GetCitiesHavingShops()
        {
            var citiesHavingShops = (from c in context.Cities
                                    join a in context.Adresses on c.CityId equals a.City.CityId
                                    join s in context.Shops on a.AddressId equals s.Adress.AddressId
                                    select c.Name).Distinct();
            return citiesHavingShops.ToList();
        }

        public IEnumerable<String> GetDepartmentsHavingShops()
        {
            var departmentsHavingShops = (from d in context.Departments
                                         join c in context.Cities on d.DepartmentCode equals c.DepartmentCode
                                         join a in context.Adresses on c.CityId equals a.City.CityId
                                         join s in context.Shops on a.AddressId equals s.Adress.AddressId
                                         select d.DepartmentName).Distinct();
            return departmentsHavingShops.ToList();
        }

        public IEnumerable<String> GetProvincesHavingShops()
        {
            var provincesHavingShops = (from p in context.Provinces
                                       join d in context.Departments on p.ProvinceCode equals d.ProvinceCode
                                       join c in context.Cities on d.DepartmentCode equals c.DepartmentCode
                                       join a in context.Adresses on c.CityId equals a.City.CityId
                                       join s in context.Shops on a.AddressId equals s.Adress.AddressId
                                       select p.Name).Distinct();

            
            return provincesHavingShops.ToList();
        }

        public IEnumerable<Shop> GetShopsFromACity(string cityName)
        {
            var shops = from s in context.Shops
                        join a in context.Adresses on s.Adress.AddressId equals a.AddressId
                        join c in context.Cities on a.City.CityId equals c.CityId
                        where c.Name == cityName
                        select s;

            return shops.ToList();
        }

        public IEnumerable<Shop> GetShopsFromADepartment(string departmentName)
        {
            var shops = from s in context.Shops
                        join a in context.Adresses on s.Adress.AddressId equals a.AddressId
                        join c in context.Cities on a.City.CityId equals c.CityId
                        join d in context.Departments on c.Department.DepartmentId equals d.DepartmentId
                        where d.DepartmentName == departmentName
                        select s;
            return shops.ToList();
        }

        public IEnumerable<Shop> GetShopsFromAProvince(string provinceName)
        {
            var shops = from s in context.Shops
                        join a in context.Adresses on s.Adress.AddressId equals a.AddressId
                        join c in context.Cities on a.City.CityId equals c.CityId
                        join d in context.Departments on c.Department.DepartmentId equals d.DepartmentId
                        join p in context.Provinces on d.Province.ProvinceId equals p.ProvinceId
                        where p.Name == provinceName
                        select s;
            return shops.ToList();
        }

        public IEnumerable<Shop> FilterShopByCity(City city)
        {
            var shops = from s in context.Shops
                        join a in context.Adresses on s.Adress.AddressId equals a.AddressId
                        join c in context.Cities on a.City.CityId equals c.CityId
                        where c.CityId == city.CityId
                        select s;

            return shops.ToList();
        }

    }
}
