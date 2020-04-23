using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_3___Press_Project
{
    public partial class AutomaticOrder
    {

        public AutomaticOrder CreateAutomaticOrderInDB(Shop selectedShop, Newspaper newspaper, DateTime startDate, DateTime endDate, int quantity)
        {
            User user = UserSingleton.GetInstance.User;
            Shop shop = selectedShop;
            AutomaticOrder automaticOrder = AutomaticOrderFactory.CreateAutomaticOrder(user, shop, newspaper, startDate, endDate, quantity);
            automaticOrder.SaveInDB();
            return automaticOrder;
        }

        private void SaveInDB()
        {
            using (var context = new PressContext())
            {
                context.Update(this);
                context.SaveChanges();
            }
        }

        public void CreateCommandsWithAutomaticOrders(Catalog catalog)
        {
            List<AutomaticOrder> automaticOrders = new List<AutomaticOrder>();
            automaticOrders = GetAutomaticOrdersFromACatalog(catalog);
            
            if(automaticOrders.Count() > 0)
            {
                foreach (AutomaticOrder automaticOrder in automaticOrders)
                {
                    OnAutomaticOrderCreateCommand(automaticOrder, catalog);
                }
            }
        }

        public static List<AutomaticOrder> GetAutomaticOrdersFromACatalog(Catalog catalog)
        {
            List<AutomaticOrder> automaticOrders = new List<AutomaticOrder>();
            using (var context = new PressContext())
            {
                var tempAutomaticOrders = (from ao in context.AutomaticOrders
                                           where ao.StartingDate <= catalog.PublicationDate 
                                                && catalog.PublicationDate <= ao.EndDate 
                                                && ao.Newspaper.NewspaperId == catalog.Newspaper.NewspaperId
                                           select ao).ToList();

                var newspapers = (from n in context.Newspapers
                                 select n).ToList();

                var shops = (from s in context.Shops
                             select s).ToList();

                var users = (from u in context.Users
                             select u).ToList();

                automaticOrders = (from ao in tempAutomaticOrders
                                   join n in newspapers on ao.Newspaper.NewspaperId equals n.NewspaperId
                                   join s in shops on ao.Shop.ShopId equals s.ShopId
                                   join u in users on ao.User.UserId equals u.UserId
                                   select ao).ToList();
            }
            return automaticOrders;
        }

        private void OnAutomaticOrderCreateCommand(AutomaticOrder automaticOrder, Catalog catalog)
        {
            OrderCatalog orderCatalog = OrderCatalogFactory.CreateOrderCatalogForAutomaticOrder(automaticOrder, catalog);
            Order order = OrderFactory.CreateForAutomaticOrder(orderCatalog, automaticOrder);
            order.SaveInDb(orderCatalog);
        }

    }
}
