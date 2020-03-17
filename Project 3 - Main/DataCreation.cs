﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Project_3___Press_Project
{
    static class DataCreation
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

                List<Editor> editors = new List<Editor>();
                editors = DataCreation.CreateEditors(3);

                List<Newspaper> newspapers = new List<Newspaper>();
                newspapers = DataCreation.CreateNewspapers(editors, 3);
                ////////////////////////////////////////////////////////////////////////////////////
                List<Catalog> catalogs = new List<Catalog>();
                catalogs = DataCreation.CreateCatalogs(newspapers, 2);

                List<OrderCatalog> orderCatalogs = new List<OrderCatalog>();
                orderCatalogs = DataCreation.CreateOrderCatalogs(catalogs);

                

                List<User> users = new List<User>();
                users = DataCreation.CreateUsers(100);

                List<UserShop> userShops = new List<UserShop>();
                userShops = DataCreation.LinkUserToShop(users, shops);

                List<ShopCatalog> shopCatalogs = new List<ShopCatalog>();
                shopCatalogs = DataCreation.CreateShopCatalogs(shops, catalogs);

                List<Order> orders = new List<Order>();
                orders = DataCreation.CreateOrders(orderCatalogs, users, userShops);

                DataCreation.LinkOrderToShop(orders, shops);

                context.AddRange(countries);
                context.AddRange(provinces);
                context.AddRange(cities);
                context.AddRange(adresses);
                context.AddRange(shops);
                context.AddRange(editors);
                context.AddRange(newspapers);
                context.AddRange(catalogs);
                context.AddRange(orderCatalogs);
                context.AddRange(orders);
                context.AddRange(users);
                context.AddRange(userShops);
                context.AddRange(shopCatalogs);

                context.SaveChanges();
                Console.WriteLine("fini");
            }
        }

        public static List<Country> CreateCountries(int numberOfCountries)
        {
            List<Country> countries = new List<Country>();

            for (int i = 0; i < numberOfCountries; i++)
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
            foreach (Country country in countries)
            {
                List<Province> provincesPerCountry = new List<Province>();
                for (int i = 0; i < numberOfProvincePerCountry; i++)
                {
                    Province province = new Province();
                    province.Name = $"Province n° {provincesCounter};{numberOfProvincePerCountry}";
                    province.Country = country;
                    provinces.Add(province);
                    provincesPerCountry.Add(province);
                    provincesCounter++;
                }
                country.Province = provincesPerCountry;
            }
            return provinces;
        }

        public static List<City> CreateCities(List<Province> provinces, int numberOfCitiesPerProvince)
        {
            List<City> cities = new List<City>();
            int cityCounter = 0;
            foreach (Province province in provinces)
            {
                List<City> citiesPerProvince = new List<City>();
                for (int i = 0; i < numberOfCitiesPerProvince; i++)
                {
                    City city = new City();
                    city.Name = $"city n° {cityCounter};{numberOfCitiesPerProvince}";
                    city.ZipCode = $"{1000 + numberOfCitiesPerProvince}";
                    city.Province = province;
                    cities.Add(city);
                    citiesPerProvince.Add(city);
                    cityCounter++;
                }
                province.Cities = citiesPerProvince;
            }
            return cities;
        }

        public static List<Adress> CreateAdresses(List<City> cities, int numberOfAdressesPerCity)
        {
            List<Adress> adresses = new List<Adress>();
            int adressesCounter = 0;
            foreach (City city in cities)
            {
                for (int i = 0; i < numberOfAdressesPerCity; i++)
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

        public static List<Shop> CreateShops(List<Adress> adresses)
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

        public static List<Editor> CreateEditors(int numberOfDesiredEditors)
        {
            List<Editor> editors = new List<Editor>();
            for (int i = 0; i < numberOfDesiredEditors; i++)
            {
                Editor editor = new Editor();
                editor.Name = $"Editor n° {i}";
                editors.Add(editor);
            }
            return editors;
        }

        public static List<Newspaper> CreateNewspapers(List<Editor> editors, int numberOfNewspaperPerEditor)
        {
            Random randomGenerator = new Random();
            List<Newspaper> newspapers = new List<Newspaper>();
            int editorCounter = 0;
            foreach (Editor editor in editors)
            {
                List<Newspaper> newspapersPerEditor = new List<Newspaper>();
                for (int i = 0; i < numberOfNewspaperPerEditor; i++)
                {
                    Newspaper newspaper = new Newspaper();
                    newspaper.Name = $"Newspaper n° {editorCounter}; {numberOfNewspaperPerEditor}";
                    newspaper.EAN13 = $"{editorCounter}{numberOfNewspaperPerEditor}{randomGenerator.Next(100, 10000)}";
                    newspaper.Price = randomGenerator.Next(1, 10);
                    newspaper.Editor = editor;
                    newspaper.Periodicity = randomGenerator.Next(1, 28);

                    newspapers.Add(newspaper);
                    newspapersPerEditor.Add(newspaper);
                }
                editor.Newspapers = newspapersPerEditor;
                editorCounter++;
            }
            return newspapers;
        }


        public static List<Catalog> CreateCatalogs(List<Newspaper> newspapers, int numberOfCatalogsPerNewspaper)
        {
            List<Catalog> catalogs = new List<Catalog>();
            foreach (Newspaper newspaper in newspapers)
            {
                List<Catalog> catalogsPerNewspaper = new List<Catalog>();
                for (int i = 0; i < numberOfCatalogsPerNewspaper; i++)
                {
                    Catalog catalog = new Catalog();
                    catalog.Newspaper = newspaper;
                    int periodicity = newspaper.Periodicity;
                    catalog.PublicationDate = DateTime.Today + TimeSpan.FromDays(periodicity * numberOfCatalogsPerNewspaper);

                    catalogs.Add(catalog);
                    catalogsPerNewspaper.Add(catalog);
                }
                newspaper.Catalogs = catalogsPerNewspaper;
            }
            return catalogs;
        }

        public static List<OrderCatalog> CreateOrderCatalogs(List<Catalog> catalogs)
        {
            List<OrderCatalog> orderCatalogs = new List<OrderCatalog>();
            foreach (Catalog catalog in catalogs)
            {
                Random randomGenerator = new Random();
                OrderCatalog orderCatalog = new OrderCatalog();
                orderCatalog.Catalog = catalog;
                orderCatalog.CatalogId = catalog.CatalogId;
                orderCatalog.Quantity = randomGenerator.Next(10, 1000);

                catalog.OrderCatalog = orderCatalog;

                orderCatalogs.Add(orderCatalog);
            }
            return orderCatalogs;
        }

        // Rajouter ShopId et UserId
        public static List<Order> CreateOrders(List<OrderCatalog> orderCatalogs, List<User> users, List<UserShop> userShops)
        {
            Random randomGenerator = new Random();
            List<Order> orders = new List<Order>();
            List<Shop> shopsHavingOrders = new List<Shop>();


            /*foreach(Shop shop in shops)
            {
                List<Order> ordersPerShop = new List<Order>();

            }*/

            foreach (OrderCatalog orderCatalog in orderCatalogs)
            {
                int randomUser = randomGenerator.Next(0, users.Count);
                Order order = new Order();
                // Rajouter liste d'orderCatalog, vérifier avec le CreateOrderCatalog des catalog
                order.OrderDate = orderCatalog.Catalog.PublicationDate;
                order.DeliveryDate = orderCatalog.Catalog.PublicationDate + TimeSpan.FromDays(randomGenerator.Next(1, 5));
                order.OrderCatalogs = orderCatalogs;
                orderCatalog.Order = order;
                orderCatalog.OrderId = order.OrderId;
                order.User = users[randomUser];
                order.UserId = users[randomUser].UserId;
                foreach(UserShop userShop in userShops)
                {
                    if (userShop.User == users[randomUser] && userShop.Shop != null)
                    {
                        order.Shop = userShop.Shop;
                        order.ShopId = userShop.ShopId;
                        shopsHavingOrders.Add(userShop.Shop);
                    }
                }
                orders.Add(order);
            }
            foreach(Shop shop in shopsHavingOrders)
            {
                shop.Orders = orders;
            }
            
            return orders;
        }

        public static void LinkOrderToShop(List<Order> orders, List<Shop> shops)
        {
            int ordersCount = orders.Count(); // 25
            int shopsCount = shops.Count(); // 15
            int ordersRestPerShop = ordersCount % shopsCount; // 10
            int numberOfShopsPerOrdersRest = shopsCount / ordersRestPerShop; // 1

            for (int j = 0; j < ordersRestPerShop; j++)
            {
                List<Order> ordersForAShop = new List<Order>();

                ordersForAShop.Add(orders[j]);
                shops[0].Orders = ordersForAShop;
            }

            for (int i = 0; i < shopsCount; i++)
            {
                List<Order> ordersForAShop = new List<Order>();

                for (int j = ordersRestPerShop; j < numberOfShopsPerOrdersRest; j++)
                {
                    ordersForAShop.Add(orders[j]);
                    orders[j].Shop = shops[i];
                }
                shops[i].Orders = ordersForAShop;
            }
        }


        public static List<User> CreateUsers(int numberOfUsers)
        {
            List<User> users = new List<User>();
            for (int i = 0; i < numberOfUsers; i++)
            {
                User user = new User();
                user.Name = $"user n° {i}";
                user.Login = "user";
                user.Password = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4"; // mdp = 1234 crypté SHA 256
                user.Function = "userFunction";
                users.Add(user);
            }
            return users;
        }
        
        public static List<UserShop> LinkUserToShop(List<User> users, List<Shop> shops)
        {
            List<UserShop> userShops = new List<UserShop>();
            int usersCount = users.Count();
            int usersPerShops = usersCount/(shops.Count());
            int restUsers = usersCount % (shops.Count());
            int counter = 0;

            foreach (Shop shop in shops)
            {
                for (int i = 0; i < usersPerShops; i++)
                {
                    UserShop userShop = new UserShop();
                    userShop.Shop = shop;
                    userShop.ShopId = shop.ShopId;
                    userShop.User = users[i + counter];
                    userShop.UserId = users[i + counter].UserId;
                    userShops.Add(userShop);
                }
                counter += usersPerShops;
            }
            if (restUsers > 0)
            {
                for (int i = 0; i < restUsers; i++)
                {
                    UserShop userShop = new UserShop();
                    userShop.Shop = shops[i];
                    userShop.ShopId = shops[i].ShopId;
                    userShop.User = users[usersPerShops + i];
                    userShop.UserId = users[usersPerShops + i].UserId;
                    userShops.Add(userShop);
                    counter ++;
                }
            }
            return userShops;
        }

        public static List<ShopCatalog> CreateShopCatalogs(List<Shop> shops, List<Catalog> catalogs)
        {
            
            List<ShopCatalog> shopCatalogs = new List<ShopCatalog>();
            Random randomGenerator = new Random();
            int i = 0;

            foreach (Shop shop in shops)
            {
                List<ShopCatalog> tempShopCatalogs = new List<ShopCatalog>();
                foreach (Catalog catalog in catalogs)
                {
                    catalog.ShopCatalogs = new List<ShopCatalog>();
                    ShopCatalog shopCatalog = new ShopCatalog();
                    shopCatalog.Shop = shop;
                    shopCatalog.ShopId = shop.ShopId;
                    shopCatalog.Catalog = catalog;
                    shopCatalog.CatalogId = catalogs[i].CatalogId;
                    shopCatalog.Quantity = randomGenerator.Next(0, 1000);
                    tempShopCatalogs.Add(shopCatalog);
                    shopCatalogs.Add(shopCatalog);
                    catalog.ShopCatalogs.Add(shopCatalog);
                }
                shop.ShopCatalogs = tempShopCatalogs;
         
            }
            return shopCatalogs;
        }
    }
}
