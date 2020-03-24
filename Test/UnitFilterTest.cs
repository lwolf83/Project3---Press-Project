using NUnit.Framework;
using System.Collections.Generic;

namespace Project_3___Press_Project
{
    public class FilterTests
    {
       /* private List<Shop> Shops { get; set; } = new List<Shop>();

        [SetUp]
        public void Setup()
        {
            Country country1 = new Country { Name = "France" };
            Country country2 = new Country { Name = "Espagne" };
            Province province1 = new Province { Name = "Grand-Est" , Country = country1 };
            Province province2 = new Province { Name = "Catalogne", Country = country2 };
            Department department1 = new Department { DepartmentName = "Haut-Rhin", Province = province1 };
            Department department2 = new Department { DepartmentName = "Pyrenées", Province = province2 };

            // Laurent: Rajout de 2 départments. Dans le "for" suivant, remplacement des provinces par départements et currentDepartment

            for (int i = 0; i < 10; i++)
            {
                Department currentDepartment = i < 5 ? department1 : department2;

                City currentCity = new City { Name = "City" + i, ZipCode = i.ToString(), Department = currentDepartment };

                Adress currentAdress = new Adress { City = currentCity, StreetName = "Street " + i, StreetNumber = i.ToString() };
                Shop currentShop = new Shop { Name = "Shop " + i, Adress = currentAdress };
                Shops.Add(currentShop);

                currentAdress = new Adress { City = currentCity, StreetName = "Street 2 - " + i, StreetNumber = i.ToString() };
                currentShop = new Shop { Name = "Shop 2 - " + i, Adress = currentAdress };
                Shops.Add(currentShop);
            }
            
        }

        [Test]
        public void TestShopFilterByCity()
        {
            List<Shop> shopResult;

            shopResult = new List<Shop>() { Shops[6], Shops[7] } ;
            Assert.AreEqual(shopResult, ShopFilter.ByCity(Shops,"City3"));
            
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
            shopResult = new List<Shop>() { Shops[0], Shops[1], Shops[2], Shops[3], Shops[4], Shops[5], Shops[6], Shops[7], Shops[8], Shops[9]};
            Assert.AreEqual(shopResult, ShopFilter.ByCountry(Shops, "Grand-Est"));

            shopResult = new List<Shop>() {};
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
        }*/
    }
}