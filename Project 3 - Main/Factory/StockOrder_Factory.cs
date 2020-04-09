using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_3___Press_Project
{
    public class StockOrder_Factory
    {
        /*       1) Add a button "Order next edition" with a TextBox for Quantity. 
         *       2) If no item selected - show OK DialogBox with a message.
         *       3) Check if editions with PublicationDate > DateTime.Now exsit
         *       4) If positive, use CreateOrder() method From OrderAction.cs to order the 
         *          first of them.      
            */

        /*private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Shop shop = (Shop)cmbShops.SelectedItem;
            Catalog catalog = (Catalog)cmbCatalog.SelectedItem;

            if (IsValidOrderParameters(shop, catalog, txtQuantity.Text))
            {
                int quantity = Convert.ToInt32(txtQuantity.Text);
                OrderAction.CreateOrder(shop, catalog, quantity);
                DisplayCurrent();
            }
        } */

        // Should be moved to a class in Project_3___Press_Project
        public bool IsValidOrderParameters(Shop shop, Catalog catalog, string quantity)
        {
            bool shopOk = !(shop is null);
            bool catalogOk = !(catalog is null);

            bool quantityOk = !String.IsNullOrWhiteSpace(quantity);
            quantityOk = quantityOk && quantity.All(char.IsDigit);
            quantityOk = quantityOk && Convert.ToInt32(quantity) < 200;

            return shopOk && catalogOk && quantityOk;
        }

        // From OrderAction.cs
        /*public static bool CreateOrder(Shop shop, Catalog catalog, int quantity)
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
        } */

        // From OrderFactory.cs
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

    }
    
}
