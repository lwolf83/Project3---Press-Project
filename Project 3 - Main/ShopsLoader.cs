using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_3___Press_Project
{
    public static class ShopsLoader
    {
        public static IEnumerable<Shop> GetAll()
        {
            var context = new PressContext();

            IEnumerable<Shop> shopList = (from s in context.Shops.AsEnumerable()
                                          join a in context.Adresses.AsEnumerable() on s.Adress.AddressId equals a.AddressId
                                         join c in context.Cities.AsEnumerable() on a.City.CityId equals c.CityId
                                         join d in context.Departments.AsEnumerable() on c.Department.DepartmentId equals d.DepartmentId
                                         join p in context.Provinces.AsEnumerable() on d.Province.ProvinceId equals p.ProvinceId
                                         join co in context.Countries.AsEnumerable() on p.Country.CountryId equals co.CountryId
                                         orderby s.Name
                                         select s).ToList();

            return shopList;
        }

        public static Shop FromUserShop(UserShop us)
        {
            Shop shop = null;
            using (var context = new PressContext())
            {
                shop = (from s in context.Shops
                        where s.ShopId == us.ShopId
                        select s).First();
            }
            return shop;
        }

        public static IEnumerable<Shop> FromUser(User user)
        {
            using (var context = new PressContext())
            {
                var currentShop = (from u in context.UserShops
                                   join s in context.Shops on u.ShopId equals s.ShopId
                                   where u.UserId == user.UserId
                                   select s).ToList();

                return currentShop;
            }
        }

    }
}
