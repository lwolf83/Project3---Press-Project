using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Project_3___Press_Project.Entity;
using Project_3___Press_Project.Filter;

namespace TestPressProject
{
    public class TestShopFiltering
    {
        [Test]
        public void TestFindShopByCity()
        {
            List<Shop> shopsTest = new List<Shop>()
            {
                new Shop { Address = new PostalAddress
                    {
                        City = new City { Name = "in" }
                    }
                },
                new Shop { Address = new PostalAddress
                    {
                        City = new City { Name = "out" }
                    }
                }
            };

            var shopFilter = new ShopFilter(shopsTest);
            shopFilter["city"] = "in";

            Assert.AreEqual(1, shopFilter.Results.Count);
            Assert.IsTrue(shopFilter.Results.Contains(shopsTest[0]));
        }
        
        [Test]
        public void TestFindShopByWrongCity()
        {
            List<Shop> shopsTest = new List<Shop>()
            {
                new Shop { Address = new PostalAddress
                    {
                        City = new City { Name = "out" }
                    }
                }
            };

            var shopFilter = new ShopFilter(shopsTest)
            {
                ["city"] = "in"
            };

            Assert.AreEqual(0, shopFilter.Results.Count);
        }

        [Test]
        public void TestFindShopByCityAndName()
        {
            List<Shop> shopsTest = new List<Shop>()
            {
                new Shop { Address = new PostalAddress
                    {
                        City = new City { Name = "in" }
                    },
                    Name = "in"
                },
                new Shop { Address = new PostalAddress
                    {
                        City = new City { Name = "out" }
                    },
                    Name = "out"
                }
            };

            var shopFilter = new ShopFilter(shopsTest);
            shopFilter["city"] = "in";
            shopFilter["name"] = "in";

            Assert.AreEqual(1, shopFilter.Results.Count);
            Assert.IsTrue(shopFilter.Results.Contains(shopsTest[0]));
        }
        
        [Test]
        public void TestShopFilteredByDepartments()
        {
            List<Shop> shopsTest = new List<Shop>()
            {
                new Shop { Address = new PostalAddress
                    {
                        City = new City { Department = new Department
                        {
                            Name = "in"
                        }}
                    }
                },
                new Shop { Address = new PostalAddress
                    {
                        City = new City { Department = new Department
                        {
                            Name = "out"
                        }}
                    }
                }
            };

            var shopFilter = new ShopFilter(shopsTest);
            shopFilter["department"] = "in";

            Assert.AreEqual(1, shopFilter.Results.Count);
            Assert.IsTrue(shopFilter.Results.Contains(shopsTest[0]));
        }
        
        /*
        [Test]
        public void TestShopFilteredByWrongDepartments()
        {
            List<Shop> shopsTest = new List<Shop>() { Shops[0], Shops[1], Shops[2], Shops[3], Shops[4], Shops[5], Shops[6], Shops[7] };
            Shop shopFilter = new Shop();
            Department Doubs = (from d in Departments
                                   where d.DepartmentName == "Doubs"
                                   select d).First();
            IEnumerable<Shop> filteredShops = shopFilter.GetShopsFromADepartment(Shops, Doubs);

            Assert.AreNotEqual(shopsTest, filteredShops);
        }

        [Test]
        public void TestShopFilteredByProvince()
        {
            List<Shop> shopsTest = new List<Shop>();
            for(int i=0; i<16; i++)
            {
                shopsTest.Add(Shops[i]);
            }
            List<Shop> ordererShopsTest = (from s in shopsTest
                                          orderby s.Name
                                          select s).ToList();
            Shop shopFilter = new Shop();
            Province alsace = (from p in Provinces
                                 where p.Name == "Alsace"
                                 select p).First();
            List<Shop> filteredShops = shopFilter.GetShopsFromAProvince(Shops, alsace).ToList();

            Assert.AreEqual(ordererShopsTest, filteredShops);
        }

        [Test]
        public void TestShopFilteredByWrongProvince()
        {
            List<Shop> shopsTest = new List<Shop>();
            for (int i = 0; i < 16; i++)
            {
                shopsTest.Add(Shops[i]);
            }
            List<Shop> ordererShopsTest = (from s in shopsTest
                                           orderby s.Name
                                           select s).ToList();
            Shop shopFilter = new Shop();
            Province francheComte = (from p in Provinces
                               where p.Name == "Franche-Comté"
                               select p).First();
            IEnumerable<Shop> filteredShops = shopFilter.GetShopsFromAProvince(Shops, francheComte);

            Assert.AreNotEqual(ordererShopsTest, filteredShops);
        }

        [Test]
        public void TestGetProvincesHavingShops()
        {
            List<Shop> shopsOfAlsace = new List<Shop>() { Shops[1], Shops[11] };
            List<Province> alsace = (from p in Provinces
                                      where p.Name == "Alsace"
                                      select p).ToList();
            Shop shopFilter = new Shop();
            List<Province> getProvinceFromShops = shopFilter.GetProvincesHavingShops(shopsOfAlsace).ToList();
            Assert.AreEqual(getProvinceFromShops, alsace);
        }

        [Test]
        public void TestGetWrongProvincesHavingShops()
        {
            List<Shop> shopsOfAlsace = new List<Shop>() { Shops[1], Shops[11] };
            List<Province> francheComte = (from p in Provinces
                                     where p.Name == "Franche-Comté"
                                     select p).ToList();
            Shop shopFilter = new Shop();
            List<Province> getProvinceFromShops = shopFilter.GetProvincesHavingShops(shopsOfAlsace).ToList();
            Assert.AreNotEqual(getProvinceFromShops, francheComte);
        }

        [Test]
        public void TestGetDepartmentsHavingShops()
        {
            List<Shop> shopsOfHautRhin = new List<Shop>() { Shops[0], Shops[6]};
            List<Department> hautRhin = (from d in Departments
                                     where d.DepartmentName == "Haut-Rhin"
                                     select d).ToList();
            Shop shopFilter = new Shop();
            List<Department> getDepartmentFromShops = shopFilter.GetDepartmentsHavingShops(shopsOfHautRhin).ToList();
            Assert.AreEqual(getDepartmentFromShops, hautRhin);
        }

        [Test]
        public void TestGetWrongDepartmentsHavingShops()
        {
            List<Shop> shopsOfHautRhin = new List<Shop>() { Shops[0], Shops[6] };
            List<Department> doubs = (from d in Departments
                                         where d.DepartmentName == "Doubs"
                                         select d).ToList();
            Shop shopFilter = new Shop();
            List<Department> getDepartmentFromShops = shopFilter.GetDepartmentsHavingShops(shopsOfHautRhin).ToList();
            Assert.AreNotEqual(getDepartmentFromShops, doubs);
        }

        [Test]
        public void TestGetCitiesHavingShops()
        {
            List<Shop> shopsOfBesanconAndStrasbourg = new List<Shop>() { Shops[16], Shops[18], Shops[9] };
            List<City> cities = new List<City>();
            cities.Add((from c in Cities
                                    where c.Name == "Besançon"
                                    select c).First());
            cities.Add((from c in Cities
                          where c.Name == "Strasbourg"
                          select c).First());
            Shop shopFilter = new Shop();
            List<City> getDepartmentFromShops = shopFilter.GetCitiesHavingShops(shopsOfBesanconAndStrasbourg).ToList();
            Assert.AreEqual(getDepartmentFromShops, cities);
        }

        [Test]
        public void TestGetWrongCitiesHavingShops()
        {
            List<Shop> shopsOfBesancon = new List<Shop>() { Shops[16], Shops[18] };
            List<City> strasbourg = (from c in Cities
                                   where c.Name == "Strasbourg"
                                   select c).ToList();
            Shop shopFilter = new Shop();
            List<City> getDepartmentFromShops = shopFilter.GetCitiesHavingShops(shopsOfBesancon).ToList();
            Assert.AreNotEqual(getDepartmentFromShops, strasbourg);
        }
        */
    }
}