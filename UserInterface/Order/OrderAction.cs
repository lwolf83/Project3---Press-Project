using System;
using System.Collections.Generic;
using Project_3___Press_Project;
using Project_3___Press_Project.Entity;
using Project_3___Press_Project.Factory;
using Project_3___Press_Project.Repository;

namespace UserInterface
{
    public static class OrderAction
    {
        public static bool CreateOrder(Shop shop, Catalog catalog, int quantity)
        {
            var orderCatalogRepository = new OrderCatalogRepository();
            Order order = OrderFactory.CreateOrLoad(UserSingleton.Instance.User, shop, DateTime.Now);

            bool orderCatalogExist = orderCatalogRepository.Exists(order, catalog);
            OrderCatalog orderCatalog;
            if (orderCatalogExist)
            {
                orderCatalog = OrderCatalogFactory.Load(order, catalog);

                string msgtext = "An order already exist. Do you want to replace it ?";
                string txt = "Overwrite Order";
                bool answserOverwrite = DialogBox.YesOrNoCancel(msgtext, txt);
                if (!answserOverwrite)
                {
                    return false;
                }
            }
            else
            {
                orderCatalog = OrderCatalogFactory.Create(order, catalog);
            }

            orderCatalog.Quantity = quantity;
            orderCatalogRepository.Update(orderCatalog);
            orderCatalog.Order.State = "In Progress";
            return true;
        }

        public static void PutCurrentInProduction()
        {
            using (var context = new PressContext())
            {
                IEnumerable<OrderCatalog> orderValidated = UserSingleton.Instance.GetOrderCatalogs();
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
