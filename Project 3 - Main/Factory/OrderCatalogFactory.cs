using System;
using System.Linq;

namespace Project_3___Press_Project
{
    public static class OrderCatalogFactory
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

        public static OrderCatalog CreateOrderCatalogForAutomaticOrder(AutomaticOrder automaticOrder, Catalog catalog)
        {
            OrderCatalog orderCatalog = new OrderCatalog();
            orderCatalog.Catalog = catalog;
            orderCatalog.CatalogId = catalog.CatalogId;
            orderCatalog.CreatedAt = DateTime.Now;
            orderCatalog.Quantity = automaticOrder.Quantity;
            return orderCatalog;
        }

    }
}
