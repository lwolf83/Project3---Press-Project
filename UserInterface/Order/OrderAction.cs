using System;
using System.Collections.Generic;
using System.Text;
using Project_3___Press_Project;

namespace UserInterface
{
    public class OrderAction
    {
        public static bool CreateOrder(Shop shop, Catalog catalog, int quantity)
        {
            Order order = OrderFactory.CreateOrLoad(UserSingleton.GetInstance.User, shop, DateTime.Now);

            bool orderCatalogExist = OrderCatalog.Exist(order, catalog);
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
            orderCatalog.Save();
            orderCatalog.ActivateOrder("In progress");
            return true;
        }

        public static void PutCurrentInProduction()
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
