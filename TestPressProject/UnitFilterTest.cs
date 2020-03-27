using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_3___Press_Project
{
    public class FilterTests
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

            List<City> cities = new List<City> { colmar, mulhouse, strasbourg, cronenbourg, besancon, burnevillers, dole, arbois };

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
                Shop shop1 = new Shop { Adress = address, Name = $"shop n°{shopCounter}" };
                shopCounter = shopCounter + 1;
                Shops.Add(shop1);
                Shop shop2 = new Shop { Adress = address, Name = $"shop n°{shopCounter}" };
                shopCounter = shopCounter + 1;
                Shops.Add(shop2);
            }
        }

        [Test]
        public void TestShopFilterByCity()
        {
            List<Shop> shopsTest = new List<Shop>() { Shops[0], Shops[1], Shops[2], Shops[3] };
            ShopFilter shopFilter = new ShopFilter();
            City colmar = (from c in Cities
                           where c.Name == "Colmar"
                           select c).First();
            IEnumerable<Shop> shopsInStrasbourg = shopFilter.GetShopsFromACity(Shops, colmar);
            Assert.AreEqual(shopsTest, shopsInStrasbourg);

            Assert.IsTrue(true);
            List<Shop> shopResult;

            shopResult = new List<Shop>() { Shops[6], Shops[7] };
            Assert.AreEqual(shopResult, ShopFilter.ByCity(Shops, "City3"));

            shopResult = new List<Shop>() { Shops[6], Shops[9] };
            Assert.AreNotEqual(shopResult, ShopFilter.ByCity(Shops, "City3"));

            shopResult = new List<Shop>();
            Assert.AreEqual(shopResult, ShopFilter.ByCity(Shops, "Cyty3"));

            shopResult = Shops;
            Assert.AreEqual(shopResult, ShopFilter.ByCity(Shops, ""));
        }

        [Test]
        public void TestShopFilterByProvince()
        {
            List<Shop> shopResult;
            shopResult = new List<Shop>() { Shops[0], Shops[1], Shops[2], Shops[3], Shops[4], Shops[5], Shops[6], Shops[7], Shops[8], Shops[9] };
            Assert.AreEqual(shopResult, ShopFilter.ByCountry(Shops, "Grand-Est"));

            shopResult = new List<Shop>() { };
            Assert.AreEqual(shopResult, ShopFilter.ByProvince(Shops, "Gra-Est"));

            shopResult = Shops;
            Assert.AreEqual(shopResult, ShopFilter.ByProvince(Shops, ""));
        }

        [Test]
        public void TestShopFilterByCountry()
        {
            List<Shop> shopResult;
            shopResult = new List<Shop>() { Shops[0], Shops[1], Shops[2], Shops[3], Shops[4], Shops[5], Shops[6], Shops[7], Shops[8], Shops[9] };
            Assert.AreEqual(shopResult, ShopFilter.ByProvince(Shops, "France"));

            shopResult = new List<Shop>() { };
            Assert.AreEqual(shopResult, ShopFilter.ByProvince(Shops, "Fra"));

            shopResult = Shops;
            Assert.AreEqual(shopResult, ShopFilter.ByProvince(Shops, ""));
        }
    }
}