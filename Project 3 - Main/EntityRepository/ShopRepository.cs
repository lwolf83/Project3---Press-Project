using System;
using System.Collections.Generic;
using System.Linq;
using Project_3___Press_Project.Entity;

namespace Project_3___Press_Project.Repository
{
    public class ShopRepository : RepositoryBase<Shop>
    {
        private PressContext context;

        public ShopRepository()
        {
            context = new PressContext();
        }

        public ICollection<Shop> FindAll()
        {
            IEnumerable<Shop> shops = context.Shops.AsEnumerable();
            IEnumerable<PostalAddress> addresses = context.Adresses.AsEnumerable();
            IEnumerable<City> cities = context.Cities.AsEnumerable();
            IEnumerable<Department> departments = context.Departments.AsEnumerable();
            IEnumerable<Province> provinces = context.Provinces.AsEnumerable();
            IEnumerable<Country> countries = context.Countries.AsEnumerable();

            IEnumerable<Shop> shopList = from s in shops
                                        join a in addresses on s.Address.PostalAddressId equals a.PostalAddressId
                                        join c in cities on a.City.CityId equals c.CityId
                                        join d in departments on c.Department.DepartmentId equals d.DepartmentId
                                        join p in provinces on d.Province.ProvinceId equals p.ProvinceId
                                        join co in countries on p.Country.CountryId equals co.CountryId
                                        orderby s.Name
                                        select s;
            return shopList.ToList();
        }

        public Shop FindByName(String name)
        {
            Shop shop = _context.Shops.SingleOrDefault(s => s.Name == name);
            return shop;
        }

        public IEnumerable<City> GetCitiesHavingShops(IEnumerable<Shop> shops)
        {
            var citiesHavingShops = (from c in shops
                                     orderby c.Address.City.Name
                                     select c.Address.City).Distinct();
            return citiesHavingShops.ToList();
        }

        public IEnumerable<Department> GetDepartmentsHavingShops(IEnumerable<Shop> shops)
        {
            var departmentsHavingShops = (from s in shops
                                          orderby s.Address.City.Department.Name
                                          select s.Address.City.Department).Distinct();
            return departmentsHavingShops.ToList();
        }

        public IEnumerable<Province> GetProvincesHavingShops(IEnumerable<Shop> shops)
        {
            var provincesHavingShops = (from s in shops
                                        orderby s.Address.City.Department.Province.Name
                                        select s.Address.City.Department.Province).Distinct();
            return provincesHavingShops.ToList();
        }

        public IEnumerable<Shop> GetShopsFromACity(IEnumerable<Shop> allShops, City city)
        {
            var shops = from s in allShops
                        where s.Address.City.Name == city.Name
                        orderby s.Address.City.Name
                        orderby s.Name
                        select s;
            return shops.ToList();
        }

        public IEnumerable<Shop> GetShopsFromADepartment(IEnumerable<Shop> allShops, Department department)
        {
            var shops = from s in allShops
                        where s.Address.City.Department.Name == department.Name
                        orderby s.Name
                        select s;
            return shops.ToList();
        }

        public IEnumerable<Shop> GetShopsFromAProvince(IEnumerable<Shop> allShops, Province province)
        {
            var shops = from s in allShops
                        where s.Address.City.Department.Province.Name == province.Name
                        orderby s.Name
                        select s;
            return shops.ToList();
        }

        public IEnumerable<Shop> FilterShopByCity(City city)
        {
            var shops = from s in context.Shops
                        join a in context.Adresses on s.Address.PostalAddressId equals a.PostalAddressId
                        join c in context.Cities on a.City.CityId equals c.CityId
                        where c.CityId == city.CityId
                        select s;
            return shops.ToList();
        }

        public Shop GetShopDetail(Guid shopId)
        {
            IEnumerable<Shop> AllShops = UserSingleton.Instance.AllShops;
            return AllShops.FirstOrDefault(i => i.ShopId.Equals(shopId));
        }


        public IEnumerable<ShopCatalog> GetAllShopCatalogs()
        {
            IEnumerable<ShopCatalog> shopCatalogs = context.ShopCatalogs.AsEnumerable();
            IEnumerable<Shop> shops = context.Shops.AsEnumerable();
            IEnumerable<Catalog> catalogs = context.Catalogs.AsEnumerable();
            IEnumerable<Newspaper> newspapers = context.Newspapers.AsEnumerable();
            IEnumerable<Editor> editors = context.Editors.AsEnumerable();

            IEnumerable<ShopCatalog> dataShopCatalog = from sc in shopCatalogs
                                                        join s in shops on sc.Shop.ShopId equals s.ShopId
                                                        join cat in catalogs on sc.Catalog.CatalogId equals cat.CatalogId
                                                        join n in newspapers on cat.Newspaper.NewspaperId equals n.NewspaperId
                                                        join e in editors on n.Editor.EditorId equals e.EditorId
                                                        orderby s.Name
                                                        select sc;
            return dataShopCatalog.ToList();
        }

        public IEnumerable<ShopCatalog> GetShopCatalogsByShop(Guid ShopId)
        {
            IEnumerable<ShopCatalog> shopCatalogs = 
                                            GetAllShopCatalogs().Where(sc => sc.Shop.ShopId == ShopId);
            return shopCatalogs;
        }

        public override bool Exists(Shop entity)
        {
            throw new NotImplementedException();
        }
    }
}