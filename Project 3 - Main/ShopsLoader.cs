﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_3___Press_Project
{
    class ShopsLoader
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


    }
}
