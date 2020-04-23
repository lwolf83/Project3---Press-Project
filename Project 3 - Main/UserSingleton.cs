using System.Collections.Generic;
using System.Linq;

namespace Project_3___Press_Project
{
    public sealed class UserSingleton
    {
        private static UserSingleton instance = null;

        public User User { get; private set; } = new User();
        public bool IsAuthenticated { get; private set; } = false;
        public IEnumerable<Shop> AllShops { get; set; }
        public List<OrderCatalog>  LatestOrderCatalogs { get; set; }
        public List<Catalog> LatestCatalogs { get; set; }

        public static UserSingleton GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserSingleton();
                }
                return instance;
            }
        }

        public void Init(string login, string password)
        {
            IAuthentification authentification = new Authentification();
            User = authentification.LoginUsers(login, password);
            if(User != null)
            {
                IsAuthenticated = true;
            }
        }

        public void Disconnect()
        {
            UserSingleton.instance = null;
        }

        public IEnumerable<Shop> GetShops()
        {
            return ShopsLoader.FromUser(User);
        }

        public IEnumerable<Catalog> GetCatalog()
        {
            using (var context = new PressContext())
            {
                List<Catalog> catalogs = context.Catalogs.ToList();
                return catalogs;
            }
           
        }

        // Dans la fonction suivante, ne pas effacer le code, même si Sonar le dit car les "useless"
        // permettent de charger les dépendances de la liste.
        public IEnumerable<OrderCatalog> GetOrderCatalogs(string state = "In progress")
        {
            var context = new PressContext();

            List<Order> orders = context.Orders.Where(x => (x.UserId == UserSingleton.GetInstance.User.UserId) && (x.State == state)).ToList();

            List<OrderCatalog> orderCatalogs = (from o in orders
                                                join oc in context.OrderCatalogs.AsEnumerable()
                                                on o.OrderId equals oc.OrderId
                                                select oc).ToList();
            
            List<Shop> shops = (from o in orders
                                join s in context.Shops.AsEnumerable()
                                on o.ShopId equals s.ShopId
                                select s).ToList();

            List<Catalog> catalog = context.Catalogs.ToList();
            List<Newspaper> newspapers = context.Newspapers.ToList();


            return orderCatalogs.OrderByDescending(o => o.CreatedAt);
        }

        public Order GetCurrentOrder(Shop shop)
        {
            var context = new PressContext();

            List<Order> dbOrder = context.Orders.Where(o => o.State == "In progress" && o.UserId == User.UserId && o.ShopId == shop.ShopId).ToList();
            if(dbOrder.Count > 0)
            {
                return dbOrder[0];
            }
            else
            {
                Order order = new Order();
                order.State = "In progress";
                order.Shop = shop;
                order.ShopId = shop.ShopId;
                order.User = UserSingleton.GetInstance.User;
                order.UserId = UserSingleton.GetInstance.User.UserId;
                context.Orders.Update(order);
                context.SaveChanges();
                return order;
            }

        }

    }
}
