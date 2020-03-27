using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class OrderCatalogAction
    {
        public static void Add(Shop shop, Catalog catalog, int quantity)
        {
            using (var context = new PressContext())
            {
                Order order = UserSingleton.GetInstance.GetCurrentOrder(shop);

               

                OrderCatalog newOrder = new OrderCatalog();
                newOrder.CatalogId = catalog.CatalogId;
                newOrder.Quantity = quantity;
                newOrder.OrderId = order.OrderId;
                newOrder.Order = order;
                context.OrderCatalogs.Update(newOrder);
                context.SaveChanges();
            }

        }

    }
}
