using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Project_3___Press_Project
{
    public class ContextPopulator
    {
        private PressContext context;

        public ContextPopulator()
        {
            context = new PressContext();
        }

        public void Populate()
        {
            /*
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            List<City> cities = new List<City>();
            cities = ContextPopulator.PopulateDBLocations_FromCSV(context);

            Random randomGenerator = new Random();
            List<City> randomCities = new List<City>();
            IEnumerable<City> DBcities = context.Cities.AsEnumerable();
            for (int counter = 0; counter < 15; counter++)
            {
                randomCities.Add(DBcities.ElementAt(counter));
            }
            Console.WriteLine(randomCities[0].Name);
            Console.WriteLine(randomCities[1].Name);
            Console.WriteLine(randomCities[2].Name);

            List<Address> addresses = ContextPopulator.CreateAddresses(randomCities, 1);
            //IEnumerable<Address> DBAdresses = ;

            List<Shop> shops = new List<Shop>();
            shops = ContextPopulator.CreateShops(addresses);

            List<Editor> editors = new List<Editor>();
            editors = ContextPopulator.CreateEditors(3);

            List<Newspaper> newspapers = new List<Newspaper>();
            newspapers = ContextPopulator.CreateNewspapers(editors, 3);

            List<Catalog> catalogs = new List<Catalog>();
            catalogs = ContextPopulator.CreateCatalogs(newspapers, 2);

            List<User> users = new List<User>();
            users = ContextPopulator.CreateUsers(100);

            List<UserShop> userShops = new List<UserShop>();
            userShops = ContextPopulator.LinkUserToShop(users, shops);

            List<ShopCatalog> shopCatalogs = new List<ShopCatalog>();
            shopCatalogs = ContextPopulator.CreateShopCatalogs(shops, catalogs);

            List<Order> orders = new List<Order>();
            orders = ContextPopulator.CreateOrders(shops, userShops, users);
            context.AddRange(orders);

            // Bug sur CreateOrderCatalogsV2 et/ou LinkOrderCatalogToCatalog
            // https://docs.microsoft.com/fr-fr/ef/core/saving/concurrency
            List<OrderCatalog> orderCatalogs = new List<OrderCatalog>();
            orderCatalogs = ContextPopulator.CreateOrderCatalogs(orders);

            ContextPopulator.LinkOrderCatalogToCatalog(orderCatalogs, catalogs);

            context.AddRange(shops);
            context.AddRange(editors);
            context.AddRange(newspapers);
            context.AddRange(catalogs);
            context.AddRange(userShops);
            context.AddRange(shopCatalogs);
            context.AddRange(users);

               

            context.AddRange(orderCatalogs);

            context.SaveChanges();
            Console.WriteLine("fini");*/

            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            /*string france = "France";
            List<String> countries = new List<string>();
            countries.Add(france);
            ContextPopulator populator = new ContextPopulator();
            populator.CreateCountry();
            populator.ImportProvincesInDB();
            populator.ImportDepartmentsInDB();
            populator.ImportCitiesInDB();*/

            //List<City> citiesInMemory = context.Cities.ToList();

            foreach(City city in context.Cities.ToList())
            {
                //city.Department = context.Departments.Where(d => d.DepartmentCode.Equals(city.DepartmentCode));
                var dpt = from Departments in context.Departments
                          join Cities in context.Cities on Departments.DepartmentCode equals Cities.DepartmentCode
                          where ((Departments.DepartmentCode == Cities.DepartmentCode) && (Cities.CityId == city.CityId))
                          select new Department() {DepartmentCode = Departments.DepartmentCode, DepartmentId = Departments.DepartmentId, DepartmentName = Departments.DepartmentName };

                city.Department = dpt.FirstOrDefault();

                /*SELECT Department.DepartmentId FROM Departments
                INNER JOIN Cities ON cities.departmentId = departments.departmentId
                Where departmentId = cities.DepartmentId*/
            }

            context.SaveChanges();
        }

        public void CreateCountry()
        {
            Country country = new Country { Name = "France" };
            context.Add(country);
            
            context.SaveChanges();
        }

        public void ImportProvincesInDB()
        {
            CSVImporter importer = new CSVImporter();
            importer.BaseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            IEnumerable<Province> provinces = importer.ImportProvinces();

            context.AddRange(provinces);
            context.SaveChanges();


            //context.Countries.First().Province = provinces.ToList();
            foreach (Province province in context.Provinces)
            {
                Console.WriteLine(provinces);
                //province.Country = context.Countries.First();
            }
            context.SaveChanges();
            
        }

        public void ImportDepartmentsInDB()
        {
            CSVImporter importer = new CSVImporter();
            importer.BaseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            IEnumerable<Department> departments = importer.ImportDepartments();

            context.AddRange(departments);
            context.SaveChanges();

        }

        public void ImportCitiesInDB()
        {
            CSVImporter importer = new CSVImporter();
            importer.BaseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            IEnumerable<City> cities = importer.ImportCities();

            context.AddRange(cities);
            context.SaveChanges();
        }



        public static List<Address> CreateAddresses(List<City> cities, int numberOfAdressesPerCity)
        {
            List<Address> adresses = new List<Address>();
            int adressesCounter = 0;
            foreach (City city in cities)
            {
                for (int i = 0; i < numberOfAdressesPerCity; i++)
                {
                    Address address = new Address();
                    address.StreetNumber = $"{numberOfAdressesPerCity}";
                    address.StreetName = $"street n° {adressesCounter};{numberOfAdressesPerCity}";
                    address.City = city;
                    adresses.Add(address);
                    adressesCounter++;
                }
            }
            /*using (var context = new PressContext())
            {
                context.AddRange(adresses);
                context.SaveChanges();
            }*/
            return adresses;
        }

        public static List<Shop> CreateShops(List<Address> adresses)
        {
            List<Shop> shops = new List<Shop>();
            int shopCounter = 0;
            foreach (Address adress in adresses)
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

        public static List<OrderCatalog> CreateOrderCatalogs(List<Order> orders)
        {
            List<OrderCatalog> orderCatalogs = new List<OrderCatalog>();
            Random randomGenerator = new Random();

            foreach(Order order in orders)
            {
                List<OrderCatalog> tempOrderCatalog = new List<OrderCatalog>();
                for (int counter = 0; counter<3; counter++)
                {
                    OrderCatalog orderCatalog = new OrderCatalog();
                    orderCatalog.Order = order;
                    orderCatalog.OrderId = order.OrderId;
                    orderCatalog.Quantity = randomGenerator.Next(100, 1000);
                    tempOrderCatalog.Add(orderCatalog);
                    orderCatalogs.Add(orderCatalog);
                }
                order.OrderCatalogs = tempOrderCatalog;
            }
            return orderCatalogs;
        }

        public static void LinkOrderCatalogToCatalog(List<OrderCatalog> orderCatalogs, List<Catalog> catalogs)
        {
            int catalogCounter = 0;
            foreach(OrderCatalog orderCatalog in orderCatalogs)
            {
                if (catalogCounter >= catalogs.Count)
                {
                    catalogCounter = 0;
                }
                orderCatalog.Catalog = catalogs[catalogCounter];
                orderCatalog.CatalogId = catalogs[catalogCounter].CatalogId;
                catalogs[catalogCounter].OrderCatalog = orderCatalog;

                catalogCounter = catalogCounter + 1;
            }
        }


        public static List<Order> CreateOrders(List<Shop> shops, List<UserShop> userShops, List<User> users)
        {
            Random randomGenerator = new Random();
            List<Order> orders = new List<Order>();

            foreach (Shop shop in shops)
            {
                List<Order> temporaryOrders = new List<Order>();
                for (int orderCounterPerShop = 0; orderCounterPerShop < 3; orderCounterPerShop++)
                {
                    Order order = new Order();
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

        public static void LinkOrderToOrderCatalog(List<Order> orders, List<OrderCatalog> orderCatalogs)
        {
            Random randomGenerator = new Random();
            foreach (Order order in orders)
            {
                List<OrderCatalog> tempOrderCatalog = new List<OrderCatalog>();
                for (int counter = 0; counter < 3; counter++)
                {
                    int randomNumber = randomGenerator.Next(0, orderCatalogs.Count);
                    tempOrderCatalog.Add(orderCatalogs[randomNumber]);
                    orderCatalogs[randomNumber].Order = order;
                    orderCatalogs[randomNumber].OrderId = order.OrderId;
                }
                order.OrderCatalogs = tempOrderCatalog;
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
