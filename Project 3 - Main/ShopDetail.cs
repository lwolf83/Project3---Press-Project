using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_3___Press_Project
{
    public class ShopDetail
    {
        public string ShopName { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string CityName { get; set; }
        public string ZipCode { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public string CountryName { get; set; }

        public ShopDetail (Guid shopId)
        {
            using (var context = new PressContext())
            {
                var datashop = context.Shops.Where(x => x.ShopId.Equals(shopId))
                                .Join(
                                        context.Adresses,
                                        sh => sh.Adress.AddressId,
                                        ad => ad.AddressId,
                                        (sh, ad) => new
                                        {
                                            ShopName = sh.Name,
                                            ad.StreetNumber,
                                            ad.StreetName,
                                            ad.City.CityId
                                        })
                                .Join(
                                        context.Cities,
                                        sha => sha.CityId,
                                        cit => cit.CityId,
                                        (sha, cit) => new
                                        {
                                            sha.ShopName,
                                            sha.StreetNumber,
                                            sha.StreetName,
                                            CityName = cit.Name,
                                            cit.ZipCode,
                                            cit.DepartmentCode,
                                            cit.Department.DepartmentId
                                        })
                                .Join(
                                        context.Departments,
                                        sac => sac.DepartmentId,
                                        dep => dep.DepartmentId,
                                        (sac, dep) => new
                                        {
                                            sac.ShopName,
                                            sac.StreetNumber,
                                            sac.StreetName,
                                            sac.CityName,
                                            sac.ZipCode,
                                            sac.DepartmentCode,
                                            DepartementName = dep.DepartmentName,
                                            dep.Province.ProvinceId
                                        })
                                .Join(
                                        context.Provinces,
                                        sacd => sacd.ProvinceId,
                                        prov => prov.ProvinceId,
                                        (sacd, prov) => new
                                        {
                                            sacd.ShopName,
                                            sacd.StreetNumber,
                                            sacd.StreetName,
                                            sacd.CityName,
                                            sacd.ZipCode,
                                            sacd.DepartmentCode,
                                            sacd.DepartementName,
                                            prov.ProvinceCode,
                                            ProvinceName = prov.Name,
                                            CountryName = prov.Country.Name
                                        }).FirstOrDefault();

                ShopName = datashop.ShopName;
                StreetNumber = datashop.StreetName;
                StreetName = datashop.StreetName;
                CityName = datashop.CityName;
                ZipCode = datashop.ZipCode;
                DepartmentCode = datashop.DepartmentCode;
                DepartmentName = datashop.DepartementName;
                ProvinceCode = datashop.ProvinceCode;
                ProvinceName = datashop.ProvinceName;
                CountryName = datashop.CountryName;
            }
        }
    }
}

