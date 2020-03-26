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


        public IEnumerable<Shop> GetAllShops()
        {
            IEnumerable<Shop> shops = context.Shops.AsEnumerable();
            IEnumerable<Address> addresses = context.Adresses.AsEnumerable();
            IEnumerable<City> cities = context.Cities.AsEnumerable();
            IEnumerable<Department> departments = context.Departments.AsEnumerable();
            IEnumerable<Province> provinces = context.Provinces.AsEnumerable();
            IEnumerable<Country> countries = context.Countries.AsEnumerable();

            IEnumerable<Shop> shopList = from s in shops
                                        join a in addresses on s.Adress.AddressId equals a.AddressId
                                        join c in cities on a.City.CityId equals c.CityId
                                        join d in departments on c.Department.DepartmentId equals d.DepartmentId
                                        join p in provinces on d.Province.ProvinceId equals p.ProvinceId
                                        join co in countries on p.Country.CountryId equals co.CountryId
                                        orderby s.Name
                                        select s;
            return shopList.ToList();
        }

        public IEnumerable<City> GetCitiesHavingShops(IEnumerable<Shop> shops)
        {
            var citiesHavingShops = (from c in shops
                                     orderby c.Adress.City.Name
                                     select c.Adress.City).Distinct();
            return citiesHavingShops.ToList();
        }

        public IEnumerable<Department> GetDepartmentsHavingShops(IEnumerable<Shop> shops)
        {
            var departmentsHavingShops = (from s in shops
                                          orderby s.Adress.City.Department.DepartmentName
                                          select s.Adress.City.Department).Distinct();
            return departmentsHavingShops.ToList();
        }

        public IEnumerable<Province> GetProvincesHavingShops(IEnumerable<Shop> shops)
        {
            var provincesHavingShops = (from s in shops
                                        orderby s.Adress.City.Department.Province.Name
                                        select s.Adress.City.Department.Province).Distinct();
            return provincesHavingShops.ToList();
        }

        public IEnumerable<Shop> GetShopsFromACity(IEnumerable<Shop> allShops, City city)
        {
            var shops = from s in allShops
                        where s.Adress.City.Name == city.Name
                        orderby s.Adress.City.Name
                        orderby s.Name
                        select s;
            return shops.ToList();
        }

        public IEnumerable<Shop> GetShopsFromADepartment(IEnumerable<Shop> allShops, Department department)
        {
            var shops = from s in allShops
                        where s.Adress.City.Department.DepartmentName == department.DepartmentName
                        orderby s.Name
                        select s;
            return shops.ToList();
        }

        public IEnumerable<Shop> GetShopsFromAProvince(IEnumerable<Shop> allShops, Province province)
        {
            var shops = from s in allShops
                        where s.Adress.City.Department.Province.Name == province.Name
                        orderby s.Name
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
