using System;
using System.Collections.Generic;
using System.Linq;

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
            ContextPopulator populator = new ContextPopulator();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            populator.CreateCountry();
            populator.ImportProvincesInDB();
            populator.ImportDepartmentsInDB();
            populator.ImportCitiesInDB();
            populator.LinkLocations();

            populator.CreateAddresses(10);
            populator.CreateShops();
            populator.CreateEditors(3);
            populator.CreateNewspapers(3);
            populator.CreateCatalogs(2);
            populator.CreateUsers(20);
            populator.LinkUserToShop();
            populator.CreateShopCatalogs();
            populator.CreateOrders();
            populator.CreateOrderCatalogs();
            populator.CreateAutomaticOrders();
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

        public void LinkLocations()
        {
            foreach (City city in context.Cities)
            {
                var dpt = from Departments in context.Departments
                          join Cities in context.Cities on Departments.DepartmentCode equals Cities.DepartmentCode
                          where ((Departments.DepartmentCode == Cities.DepartmentCode) && (Cities.CityId == city.CityId))
                          select Departments;

                city.Department = dpt.FirstOrDefault();
            }

            foreach (Department department in context.Departments)
            {
                var prv = from Provinces in context.Provinces
                          join Departments in context.Departments on Provinces.ProvinceCode equals Departments.ProvinceCode
                          where ((Provinces.ProvinceCode == Departments.ProvinceCode) && (Departments.DepartmentId == department.DepartmentId))
                          select Provinces;

                department.Province = prv.FirstOrDefault();
            }

            foreach (Province province in context.Provinces)
            {
                province.Country = context.Countries.FirstOrDefault();
            }

            context.SaveChanges();
        }


        public void CreateAddresses(int numberOfCitiesWithAnAddress)
        {
            var randomCities = (from Cities in context.Cities
                                orderby Cities.Name ascending
                                select Cities).Take(numberOfCitiesWithAnAddress);

            Random randomGenerator = new Random();
            int adressesCounter = 0;
            foreach (City city in randomCities)
            {
                for (int i = 0; i < 10; i++)
                {
                    Address address = new Address();
                    address.StreetNumber = $"{randomGenerator.Next(1, 1000)}";
                    address.StreetName = $"street n° {adressesCounter};{randomGenerator.Next(1, 100)}";
                    address.City = city;
                    adressesCounter++;
                    context.Add(address);
                }
            }
            context.SaveChanges();
            
        }

        public void CreateShops()
        {
            var adresses = from Address in context.Adresses
                           select Address;


            int shopCounter = 0;
            foreach (Address adress in adresses)
            {
                Shop shop = new Shop();
                shop.Name = $"Shop n° {shopCounter}";
                shop.Adress = adress;
                shopCounter++;
                context.Add(shop);
            }
            context.SaveChanges();
        }

        public void CreateEditors(int numberOfDesiredEditors)
        {
            for (int i = 0; i < numberOfDesiredEditors; i++)
            {
                Editor editor = new Editor();
                editor.Name = $"Editor n° {i}";
                context.Add(editor);
            }
            context.SaveChanges();
        }

        public void CreateNewspapers(int numberOfNewspaperPerEditor)
        {
            var editors = from Editors in context.Editors
                          select Editors;

            Random randomGenerator = new Random();
            int editorCounter = 0;
            foreach (Editor editor in editors)
            {
                List<Newspaper> newspapersPerEditor = new List<Newspaper>();
                for (int i = 0; i < numberOfNewspaperPerEditor; i++)
                {
                    Newspaper newspaper = new Newspaper();
                    newspaper.Name = $"Newspaper n° {editorCounter}; {i}";
                    newspaper.EAN13 = $"{editorCounter}{numberOfNewspaperPerEditor}{randomGenerator.Next(100, 10000)}";
                    newspaper.Price = randomGenerator.Next(1, 10);
                    newspaper.Editor = editor;
                    newspaper.Periodicity = randomGenerator.Next(1, 28);

                    newspapersPerEditor.Add(newspaper);
                    context.Add(newspaper);
                }
                editor.Newspapers = newspapersPerEditor;
                editorCounter++;
            }
            context.SaveChanges();
        }


        public void CreateCatalogs(int numberOfCatalogsPerNewspaper)
        {
            var newspapers = from Newspapers in context.Newspapers
                             select Newspapers;
            int cpt = 0;
            foreach (Newspaper newspaper in newspapers)
            {
                List<Catalog> catalogsPerNewspaper = new List<Catalog>();
                for (int i = 0; i < numberOfCatalogsPerNewspaper; i++)
                {
                    Catalog catalog = new Catalog();
                    catalog.Newspaper = newspaper;
                    int periodicity = newspaper.Periodicity;
                    catalog.PublicationDate = DateTime.Today + TimeSpan.FromDays(periodicity * numberOfCatalogsPerNewspaper);
                    catalog.Name = "Edition " + cpt;
                    cpt++;
                    catalogsPerNewspaper.Add(catalog);
                    context.Add(catalog);
                }
                newspaper.Catalogs = catalogsPerNewspaper;
            }
            context.SaveChanges();
        }

        public void CreateOrderCatalogs()
        {
            Random randomGenerator = new Random();

            var orders = (from Orders in context.Orders
                         select Orders).ToList();

            var catalogs = (from Catalogs in context.Catalogs
                           select Catalogs).ToList();

            int numberOfOrdersPerCatalogs = orders.Count() / catalogs.Count();
            int restOffOrdersDividedByCatalogs = orders.Count() % catalogs.Count();
            int counter = 0;

            foreach(Catalog catalog in catalogs)
            {
                for (int i=0; i<numberOfOrdersPerCatalogs; i++)
                {
                    OrderCatalog orderCatalog = new OrderCatalog();
                    orderCatalog.CreatedAt = DateTime.Now;
                    orderCatalog.Catalog = catalog;
                    orderCatalog.CatalogId = catalog.CatalogId;
                    orderCatalog.Order = orders[counter];
                    orderCatalog.OrderId = orders[counter].OrderId;
                    orderCatalog.Quantity = randomGenerator.Next(100, 10000);
                    context.Add(orderCatalog);
                    counter = counter + 1;
                }
            }
            if(restOffOrdersDividedByCatalogs > 0)
            {
                for(int i=0; i<restOffOrdersDividedByCatalogs; i++)
                {
                    OrderCatalog orderCatalog = new OrderCatalog();
                    orderCatalog.Catalog = catalogs[i];
                    orderCatalog.CatalogId = catalogs[i].CatalogId;
                    orderCatalog.Order = orders[counter];
                    orderCatalog.OrderId = orders[counter].OrderId;
                    orderCatalog.Quantity = randomGenerator.Next(100, 10000);
                    context.Add(orderCatalog);
                    counter = counter + 1;
                }
            }
            context.SaveChanges();
        }


        public void CreateOrders()
        {
            var shops = from Shops in context.Shops
                        select Shops;

            Random randomGenerator = new Random();

            foreach(Shop shop in shops)
            {
                var users = from Users in context.Users
                            join UserShops in context.UserShops on Users.UserId equals UserShops.UserId
                            where UserShops.ShopId == shop.ShopId
                            select Users;

                List<Order> orders = new List<Order>();
                foreach(User user in users)
                {
                    Order order = new Order();
                    order.Shop = shop;
                    order.ShopId = shop.ShopId;
                    order.OrderDate = DateTime.Now - TimeSpan.FromDays(randomGenerator.Next(11,50));
                    order.DeliveryDate = order.OrderDate + TimeSpan.FromDays(randomGenerator.Next(1,10));
                    order.User = user;
                    order.UserId = user.UserId;
                    order.State = "In progress";
                    orders.Add(order);
                    context.Add(order);
                }
                shop.Orders = orders;
            }
            context.SaveChanges();
        }

        
        public void CreateUsers(int numberOfUsers)
        {
            for (int i = 0; i < numberOfUsers; i++)
            {
                User user = new User();
                user.Name = $"user{i}";
                user.Login = $"user{i}";
                user.Password = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4"; // mdp = 1234 crypté SHA 256
                user.Function = "userFunction";
                context.Add(user);
            }
            context.SaveChanges();
        }
        
        public void LinkUserToShop()
        {
            var shops = (from Shops in context.Shops
                        select Shops).ToList();

            var users = (from Users in context.Users
                        select Users).ToList();

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
                    context.Add(userShop);
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
                    context.Add(userShop);
                    counter ++;
                }
            }
            context.SaveChanges();
        }

        public void CreateShopCatalogs()
        {
            var shops = from Shops in context.Shops
                        select Shops;

            var catalogs = (from Catalogs in context.Catalogs
                           select Catalogs).ToList();

            Random randomGenerator = new Random();

            foreach(Shop shop in shops)
            {
                List<ShopCatalog> shopCatalogs = new List<ShopCatalog>();
                foreach(Catalog catalog in catalogs)
                {
                    ShopCatalog shopCatalog = new ShopCatalog();
                    shopCatalog.Catalog = catalog;
                    shopCatalog.CatalogId = catalog.CatalogId;
                    shopCatalog.Quantity = randomGenerator.Next(1, 1000);
                    shopCatalog.Shop = shop;
                    shopCatalog.ShopId = shop.ShopId;
                    shopCatalogs.Add(shopCatalog);

                    context.Add(shopCatalog);
                }
                shop.ShopCatalogs = shopCatalogs;
            }
            context.SaveChanges();

        }

        public void CreateAutomaticOrders()
        {
            var newspapers = (from n in context.Newspapers
                              select n).ToList();
            var users = (from u in context.Users
                         select u).ToList();
            var userShops = (from us in context.UserShops
                             select us).ToList();

            var shops = (from u in users
                        join us in userShops on u.UserId equals us.UserId
                        join s in context.Shops on us.ShopId equals s.ShopId
                        select s).ToList();

            Random randomGenerator = new Random();
            foreach (Shop shop in shops)
            {
                foreach(Newspaper newspaper in newspapers)
                {
                    AutomaticOrder ao = new AutomaticOrder();
                    ao.Newspaper = newspaper;
                    ao.Shop = shop;
                    ao.User = shop.UserShops.ToList()[0].User;
                    DateTime startingDate = DateTime.Now + TimeSpan.FromDays(randomGenerator.Next(1, 30));
                    ao.StartingDate = startingDate;
                    ao.EndDate = startingDate + TimeSpan.FromDays(randomGenerator.Next(100,1000));
                    ao.Quantity = randomGenerator.Next(0, 100);
                    context.Add(ao);
                }
            }
            context.SaveChanges();
        }
    }
}
