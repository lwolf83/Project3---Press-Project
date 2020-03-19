using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Project_3___Press_Project
{
    public class DataCreation
    {
        public static void CreateData()
        {
            using (var context = new PressContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                List<City> cities = new List<City>();
                cities = DataCreation.PopulateDBLocations_FromCSV(context);

                Random randomGenerator = new Random();
                List<City> randomCities = new List<City>();
                for (int counter = 0; counter < 3; counter++)
                {
                    randomCities.Add(cities[randomGenerator.Next(0, cities.Count)]);
                }

                List<Adress> adresses = new List<Adress>();
                adresses = DataCreation.CreateAdresses(randomCities, 3);

                List<Shop> shops = new List<Shop>();
                shops = DataCreation.CreateShops(adresses);

                List<Editor> editors = new List<Editor>();
                editors = DataCreation.CreateEditors(3);

                List<Newspaper> newspapers = new List<Newspaper>();
                newspapers = DataCreation.CreateNewspapers(editors, 3);

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

                context.AddRange(adresses);
                context.AddRange(shops);
                context.AddRange(editors);
                context.AddRange(newspapers);

                context.AddRange(catalogs);
                context.AddRange(orderCatalogs);
                context.AddRange(userShops);
                context.AddRange(shopCatalogs);
                context.AddRange(users);

                List<Order> orders = new List<Order>();
                orders = DataCreation.CreateOrders(shops, orderCatalogs, userShops, users);

                context.AddRange(orders);
               
                context.SaveChanges();
                Console.WriteLine("fini");
            }
        }


        public static List<City> PopulateDBLocations_FromCSV(PressContext context)
        {
            var country = new Country { Name = "France" };
            context.Add(country);

            var provincesData = CSVDataReader.GetDataEntries(@"..\..\..\..\regions.csv");
            var manyProvinces = from i in Enumerable.Range(0, provincesData.Count)
                                select new Province
                                { ProvinceCode = provincesData[i][0], Name = provincesData[i][1], Country = country };
            context.AddRange(manyProvinces);
            context.SaveChanges();

            var departmentsData = CSVDataReader.GetDataEntries(@"..\..\..\..\departments.csv");
            var manyDepartments = from i in Enumerable.Range(0, departmentsData.Count)
                                    select new Department
                                    {
                                        ProvinceCode = departmentsData[i][0],
                                        DepartmentCode = departmentsData[i][1],
                                        DepartmentName = departmentsData[i][2],
                                        Province = context.Provinces.Where(p => p.ProvinceCode.Equals(departmentsData[i][0])).First()
                                    };
            context.AddRange(manyDepartments);
            context.SaveChanges();

            var citiesData = CSVDataReader.GetDataEntries(@"..\..\..\..\cities.csv");
            var manyCities = from i in Enumerable.Range(0, citiesData.Count)
                                select new City
                                {
                                    DepartmentCode = citiesData[i][0],
                                    ZipCode = citiesData[i][1],
                                    Name = citiesData[i][2],
                                    Department = context.Departments.Where(d => d.DepartmentCode.Equals(citiesData[i][0])).First()
                                };
            context.AddRange(manyCities);
            context.SaveChanges();

            return manyCities.ToList();
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

       

        public static List<Order> CreateOrders(List<Shop> shops, List<OrderCatalog> orderCatalogs, List<UserShop> userShops, List<User> users)
        {
            Random randomGenerator = new Random();
            List<Order> orders = new List<Order>();

            foreach (Shop shop in shops)
            {
                List<Order> temporaryOrders = new List<Order>();
                for (int orderCounterPerShop = 0; orderCounterPerShop < 3; orderCounterPerShop++)
                {
                    Order order = new Order();
                    order.OrderCatalogs = orderCatalogs;
                    order.Shop = shop;
                    order.ShopId = shop.ShopId;
                    order.OrderDate = DateTime.Now + TimeSpan.FromDays(randomGenerator.Next(1, 100));
                    order.DeliveryDate = order.OrderDate + TimeSpan.FromDays(randomGenerator.Next(1, 10));
                    var usershopAvecUser = userShops.Where(x => x.ShopId == shop.ShopId).FirstOrDefault();
                    var user = users.Where(x => x.UserId == usershopAvecUser.UserId).FirstOrDefault();
                    order.User = user;
                    order.UserId = order.User.UserId;
                    orders.Add(order);
                    temporaryOrders.Add(order);
                }
                shop.Orders = temporaryOrders;
            }
            return orders;

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
