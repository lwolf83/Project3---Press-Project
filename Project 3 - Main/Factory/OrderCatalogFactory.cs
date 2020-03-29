using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public class OrderCatalogFactory
    {
        public static OrderCatalog Create(Order order, Catalog catalog)
        {
            OrderCatalog newOrder;
            using (var context = new PressContext())
            {
                newOrder = new OrderCatalog();
                newOrder.CatalogId = catalog.CatalogId;
                newOrder.Quantity = 0;
                newOrder.OrderId = order.OrderId;
                newOrder.Order = order;
                newOrder.CreatedAt = DateTime.Now;
            }

            return newOrder;
        }

        public static OrderCatalog Load(Order order, Catalog catalog)
        {
            OrderCatalog orderLoaded;
            using(var context = new PressContext())
            {
                orderLoaded = context.OrderCatalogs.Where(o => o.OrderId == order.OrderId && o.CatalogId == catalog.CatalogId).FirstOrDefault();
            }
            return orderLoaded;
        }

        public static void PutOrderCurrentInProduction()
        {
            using (var context = new PressContext())
            {
                IEnumerable<OrderCatalog> orderValidated = UserSingleton.GetInstance.GetOrderCatalogs();
                foreach (OrderCatalog orderCatalog in orderValidated)
                {
                    orderCatalog.Order.State = "In Production";
                }
                context.UpdateRange(orderValidated);
                context.SaveChanges();

            }
        }

    }
}
