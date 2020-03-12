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

                List<Country> countries = new List<Country>();
                countries = DataCreation.CreateCountries(2);

                List<Province> provinces = new List<Province>();
                provinces = DataCreation.CreateProvinces(countries, 3);

                List<City> cities = new List<City>();
                cities = DataCreation.CreateCities(provinces, 3);

                List<Adress> adresses = new List<Adress>();
                adresses = DataCreation.CreateAdresses(cities, 3);

                List<Shop> shops = new List<Shop>();
                shops = DataCreation.CreateShops(adresses);

                foreach(Country country in countries)
                { context.Add(country); }

                foreach(Province province in provinces)
                { context.Add(province); }

                foreach(City city in cities)
                { context.Add(city); }

                foreach(Adress adress in adresses)
                { context.Add(adress); }

                foreach(Shop shop in shops)
                { context.Add(shop); }

                context.SaveChanges();
            }
        }

        public static List<Country> CreateCountries(int numberOfCountries)
        {
            List<Country> countries = new List<Country>();

            for (int i = 0; i < numberOfCountries; i++ )
            {
                Country country = new Country();
                country.Name = $"Country n° {numberOfCountries}";
                countries.Add(country);
            }

            return countries;
        }

        public static List<Province> CreateProvinces(List<Country> countries, int numberOfProvincePerCountry)
        {
            List<Province> provinces = new List<Province>();
            int provincesCounter = 0;
            foreach(Country country in countries)
            {
                for (int i = 0; i<numberOfProvincePerCountry; i++)
                {
                    Province province = new Province();
                    province.Name = $"Province n° {provincesCounter};{numberOfProvincePerCountry}";
                    province.Country = country;
                    provinces.Add(province);
                    provincesCounter++;
                }
                country.Province = provinces;
            }
            return provinces;
        }

        public static List<City> CreateCities(List<Province> provinces, int numberOfCitiesPerProvince)
        {
            List<City> cities = new List<City>();
            int cityCounter = 0;
            foreach(Province province in provinces)
            {
                for (int i = 0; i<numberOfCitiesPerProvince; i++)
                {
                    City city = new City();
                    city.Name = $"city n° {cityCounter};{numberOfCitiesPerProvince}";
                    city.ZipCode = $"{1000 + numberOfCitiesPerProvince}";
                    city.Province = province;
                    cities.Add(city);
                    cityCounter++;
                }
                province.Cities = cities;
            }
            return cities;
        }

        public static List<Adress> CreateAdresses(List<City> cities, int numberOfAdressesPerCity)
        {
            List<Adress> adresses = new List<Adress>();
            int adressesCounter = 0;
            foreach(City city in cities)
            {
                for (int i = 0; i<numberOfAdressesPerCity; i++)
                {
                    Adress adress = new Adress();
                    adress.StreetNumber = $"{numberOfAdressesPerCity}";
                    adress.StreetName = $"street n° {adressesCounter};{numberOfAdressesPerCity}";
                    adress.City = city;
                    adresses.Add(adress);
                    adressesCounter++;
                }
            }
            return adresses;
        }

        public static List<Shop> CreateShops (List<Adress> adresses)
        {
            List<Shop> shops = new List<Shop>();
            int shopCounter = 0;
            foreach (Adress adress in adresses)
            {
                Shop shop = new Shop();
                shop.Name = $"Shop n° {shopCounter}";
                shop.Adress = adress;
                shopCounter++;
                shops.Add(shop);
            }
            return shops;
        }
    }
}
