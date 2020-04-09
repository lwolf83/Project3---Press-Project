using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public partial class AutomaticOrder
    {
        public static AutomaticOrder CreateAutomaticOrder(UserShop userShop, Newspaper newspaper, DateTime startDate, DateTime endDate, int quantity)
        {
            AutomaticOrder ao = new AutomaticOrder();
            ao.Newspaper = newspaper;
            ao.StartingDate = startDate;
            ao.EndDate = endDate;
            ao.User = userShop.User;
            ao.Shop = userShop.Shop;
            ao.Quantity = quantity;

            return ao;
        }

        public static void RecordNewAutomaticOrderInDB(AutomaticOrder automaticOrder)
        {
            using (var context = new PressContext())
            {
                context.Add(automaticOrder);
                context.SaveChanges();
            }
        }

        public static void OnAutomaticOrderCreateOrders(List<AutomaticOrder> automaticOrders, Catalog catalog)
        {
            foreach(AutomaticOrder automaticOrder in automaticOrders)
            {
                Catalog lastCatalog = new Catalog();
                using (var context = new PressContext())
                {
                    lastCatalog = (from c in context.Catalogs
                                   where c.Newspaper.NewspaperId == automaticOrder.Newspaper.NewspaperId
                                   orderby c.PublicationDate ascending
                                   select c).Last();
                }

                if (automaticOrder.StartingDate >= lastCatalog.PublicationDate)
                {
                    OrderCatalog orderCatalog = new OrderCatalog();
                    orderCatalog.Quantity = automaticOrder.Quantity;
                    // To continue
                }
            }
            
        }


    }
}
