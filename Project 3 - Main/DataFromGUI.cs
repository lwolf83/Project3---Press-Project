﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Project_3___Press_Project
{
    public class DataFromGUI
    {
        public static bool AddShop(string shopName, string streetNumber, string streetName, string cityName)
        {
            bool shopCreated = true;
            bool addressCreated = AddAddress(streetNumber, streetName, cityName);
            if (addressCreated)
            {
                using (var context = new PressContext())
                {
                    Adress adress = context.Adresses.AsEnumerable().Last();
                    Shop shop = new Shop();
                    shop.Name = shopName;
                    shop.Adress = adress;
                    context.Add(shop);
                    context.SaveChanges();
                }
            }
            else 
            {
                shopCreated = false;
            }
            return shopCreated;
        }

        public static bool AddAddress(string StreetNumber, string StreetName, string cityName)
        {
            bool addressCreated = true;
            using (var context = new PressContext())
            {
                List<City> cities = context.Cities.Where(d => d.Name.Equals(cityName)).ToList();
                if (cities == null || cities.Count == 0)
                {
                    addressCreated = false;
                }
                else
                {
                    Random randomGenerator = new Random();
                    Adress adress = new Adress();
                    adress.StreetNumber = StreetNumber;
                    adress.StreetName = StreetName;
                    adress.City = cities[randomGenerator.Next(0, cities.Count)];
                    context.Add(adress);
                    context.SaveChanges();
                }
            }
            return addressCreated;
        }
    }
}