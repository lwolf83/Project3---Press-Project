using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public partial class OrderCatalog
    {
        public static bool Exist(Order order, Catalog catalog)
        {
            var context = new PressContext();

            int nbOrderCatalog = context.OrderCatalogs.Where(o => o.OrderId == order.OrderId && o.CatalogId == catalog.CatalogId).Count();

            if (nbOrderCatalog > 0)
                return true;
            else
                return false;
        }

        public void Save()
        {
            var context = new PressContext();
            context.OrderCatalogs.Update(this);
            context.SaveChanges();
        }

        public bool ActivateOrder(string state)
        {
            var context = new PressContext();
            Order currentOrder = context.Orders.Where(o => o.OrderId == this.OrderId).FirstOrDefault();
            if (currentOrder.State != state)
            {
                currentOrder.State = state;
                context.Orders.Update(currentOrder);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
