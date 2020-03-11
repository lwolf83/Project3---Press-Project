using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Project_3___Press_Project
{
    class DataCreation
    {
        public static void CreateData()
        {
            using (var context = new PressContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var france = new Country();
                france.Name = "France";
                france.Province = new List<Province>();

                var regionsFr = from i in Enumerable.Range(1, 3)
                                select new Province();

                List<string> provinceNames = new List<string>() { "Alsace", "Ile de France", "Provence-Alpes-Côte d'Azur" };
                List<string> cityNames = new List<string>() { "Strasbourg", "Paris", "Marseille" };
                List<int> zipCodes = new List<int>() { 67000, 75000, 13000 };
                List<City> cities = new List<City>();
                List<Adress> adresses = new List<Adress>();
                int counter = 0;
                foreach(Province region in regionsFr)
                {
                    region.Country = france;
                    region.Name = provinceNames[counter];
                    region.Cities = new List<City>();
                    City city = new City();
                    city.Name = cityNames[counter];
                    city.ZipCode = zipCodes[counter];
                    city.Province = region;
                    cities.Add(city);

                    if (region.Name == "Alsace")
                    {
                        City Breuschwickersheim = new City();
                        Breuschwickersheim.Name = "Breuschwickersheim";
                        Breuschwickersheim.ZipCode = 67112;
                        Breuschwickersheim.Province = region;
                        region.Cities.Add(Breuschwickersheim);
                        cities.Add(Breuschwickersheim);
                    }
                    region.Cities.Add(city);
                    france.Province.Add(region);

                    context.Add(region);
                    counter++;
                }

                Random randomGenerator = new Random();
                foreach(City city in cities)
                {
                    Adress adress = new Adress();
                    adress.StreetName = "rue de l'église";
                    adress.StreetNumber = randomGenerator.Next(1, 100);
                    adress.City = city;
                    adresses.Add(adress);

                    context.Add(city);
                    context.Add(adress);
                }

                var germany = new Country();
                germany.Province = new List<Province>();
                germany.Name = "Deutschland";

                Province province = new Province();
                province.Name = "Berlin Land";
                province.Country = germany;
                province.Cities = new List<City>();

                City berlin = new City();
                berlin.Name = "Berlin";
                berlin.ZipCode = 10117;
                berlin.Province = province;

                Adress berlinerAddress = new Adress();
                berlinerAddress.StreetNumber = randomGenerator.Next(1, 100);
                berlinerAddress.StreetName = "Unter den Linden";
                berlinerAddress.City = berlin;
                adresses.Add(berlinerAddress);

                province.Cities.Add(berlin);
                germany.Province.Add(province);

                context.Add(province);
                context.Add(berlin);
                context.Add(berlinerAddress);

                context.Add(france);
                context.Add(germany);

                List<string> shopNames = new List<string>() { "Le grand bazar", "L'antre du journal", "Magazine moi", "Le kiosque à Dudule", "Das Nachbarbrötchen" };
                int counterShopNames = 0;
                foreach(Adress adress in adresses)
                {
                    Shop shop = new Shop();
                    shop.Name = shopNames[counterShopNames];
                    shop.Adress = adress;
                    counterShopNames++;
                    context.Add(shop);
                }

                context.SaveChanges();
            }
        }
    }
}
