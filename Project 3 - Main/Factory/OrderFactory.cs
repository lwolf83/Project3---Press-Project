using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public class OrderFactory
    {
        public static Order CreateOrLoad(User user, Shop shop, DateTime orderDate)
        {
            var context = new PressContext();

            List<Order> orders = context.Orders.Where(
                                    o => o.Shop.ShopId == shop.ShopId
                                    && o.User.UserId == user.UserId
                                    && o.OrderDate.Day == orderDate.Day
                                    && o.OrderDate.Month == orderDate.Month
                                    && o.OrderDate.Year == orderDate.Year
                                    ).ToList();
            Order resOrder;

            if (orders.Count > 0)
            {
                resOrder = orders.FirstOrDefault();
            }
            else
            {
                resOrder = new Order();
                resOrder.OrderDate = orderDate;
                resOrder.DeliveryDate = orderDate.AddDays(1.0);
                resOrder.Shop = shop;
                resOrder.User = user;
                resOrder.State = "In progress";
                context.Orders.Update(resOrder);
                context.SaveChanges();

            }
            return resOrder;
        }

        public static Order CreateForAutomaticOrder(OrderCatalog orderCatalog, AutomaticOrder automaticOrder)
        {
            List<OrderCatalog> orderCatalogs = new List<OrderCatalog>() { orderCatalog };
            Order order = new Order();
            order.OrderCatalogs = orderCatalogs;
            order.OrderDate = orderCatalog.CreatedAt;
            order.Shop = automaticOrder.Shop;
            order.ShopId = automaticOrder.Shop.ShopId;
            order.User = automaticOrder.User;
            order.UserId = automaticOrder.User.UserId;
            order.State = "In progress";
            return order;
        }

    }
}
