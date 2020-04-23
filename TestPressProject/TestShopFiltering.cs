using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_3___Press_Project
{
    public class TestShopFiltering
    {
        private List<Shop> Shops { get; set; } = new List<Shop>();
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<City> Cities { get; set; } = new List<City>();
        public List<Department> Departments { get; set; } = new List<Department>();
        public List<Province> Provinces { get; set; } = new List<Province>();
        public List<Country> Countries { get; set; } = new List<Country>();

        [SetUp]
        public void Setup()
        {
            Shops  = new List<Shop>();
            Addresses  = new List<Address>();
            Cities  = new List<City>();
            Departments  = new List<Department>();
            Provinces  = new List<Province>();
            Countries  = new List<Country>();

            Country franceCountry = new Country { Name = "France" };
            Countries.Add(franceCountry);

            Province alsace = new Province { Name = "Alsace", Country = franceCountry };
            Provinces.Add(alsace);
            Province francheComte = new Province { Name = "Franche-Comté", Country = franceCountry };
            Provinces.Add(francheComte);

            Department hautRhin = new Department { DepartmentName = "Haut-Rhin", Province = alsace };
            Departments.Add(hautRhin);
            Department basRhin = new Department { DepartmentName = "Bas-Rhin", Province = alsace };
            Departments.Add(basRhin);
            Department doubs = new Department { DepartmentName = "Doubs", Province = francheComte };
            Departments.Add(doubs);
            Department jura = new Department { DepartmentName = "Jura", Province = francheComte };
            Departments.Add(jura);

            City colmar = new City { Department = hautRhin, Name = "Colmar", ZipCode = "68000" };
            Cities.Add(colmar);
            City mulhouse = new City { Department = hautRhin, Name = "Mulhouse", ZipCode = "68100" };
            Cities.Add(mulhouse);
            City strasbourg = new City { Department = basRhin, Name = "Strasbourg", ZipCode = "67000" };
            Cities.Add(strasbourg);
            City cronenbourg = new City { Department = basRhin, Name = "Cronenbourg", ZipCode = "67200" };
            Cities.Add(cronenbourg);
            City besancon = new City { Department = doubs, Name = "Besançon", ZipCode = "25000" };
            Cities.Add(besancon);
            City burnevillers = new City { Department = doubs, Name = "Burnevilles", ZipCode = "25470" };
            Cities.Add(burnevillers);
            City dole = new City { Department = jura, Name = "Dole", ZipCode = "39100" };
            Cities.Add(dole);
            City arbois = new City { Department = jura, Name = "Arbois", ZipCode = "39013" };
            Cities.Add(arbois);

            List<City> cities = new List<City> { colmar, mulhouse , strasbourg, cronenbourg, besancon, burnevillers, dole, arbois };

            Random randomGenerator = new Random();
            List<Address> addresses = new List<Address>();
            foreach (City city in cities)
            {
                Address address1 = new Address { City = city, StreetName = "street", StreetNumber = randomGenerator.Next(1, 200).ToString() };
                Address address2 = new Address { City = city, StreetName = "street", StreetNumber = randomGenerator.Next(1, 200).ToString() };
                addresses.Add(address1);
                addresses.Add(address2);
            }
            Addresses = addresses;

            int shopCounter = 0;
            foreach (Address address in addresses)
            {
                Shop shop1 = new Shop { Adress = address, Name = $"shop n°{shopCounter}" + address.City.Name };
                shopCounter = shopCounter + 1;
                Shops.Add(shop1);
                Shop shop2 = new Shop { Adress = address, Name = $"shop n°{shopCounter}" + address.City.Name };
                shopCounter = shopCounter + 1;
                Shops.Add(shop2);
            }
        }

        [Test]
        public void TestShopFilterByCity()
        {
            List<Shop> shopsTest = new List<Shop>() { Shops[0], Shops[1], Shops[2], Shops[3] };
            Shop shopFilter = new Shop();
            City colmar = (from c in Cities
                           where c.Name == "Colmar"
                           select c).First();
            List<Shop> shopsInColmar = shopFilter.GetShopsFromACity(Shops, colmar).ToList();
            Assert.AreEqual(shopsTest, shopsInColmar);

        }
        
        [Test]
        public void TestShopFilterByWrongCity()
        {
            List<Shop> shopsTest = new List<Shop>() { Shops[0], Shops[1], Shops[2], Shops[3] };
            Shop shopFilter = new Shop();
            City current_strasbourg = (from c in Cities
                           where c.Name == "Strasbourg"
                           select c).First();
            IEnumerable<Shop> filteredShops = shopFilter.GetShopsFromACity(Shops, current_strasbourg);
            Assert.AreNotEqual(shopsTest, filteredShops);
        }
        
        [Test]
        public void TestShopFilteredByDepartments()
        {
            List<Shop> shopsTest = new List<Shop>() { Shops[0], Shops[1], Shops[2], Shops[3], Shops[4], Shops[5], Shops[6], Shops[7] };
            Shop shopFilter = new Shop();
            Department hautRhin = (from d in Departments
                                 where d.DepartmentName == "Haut-Rhin"
                                 select d).First();
            List<Shop> filteredShops = shopFilter.GetShopsFromADepartment(Shops, hautRhin).ToList();

            Assert.AreEqual(shopsTest, filteredShops);
        }
        
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
    }
}