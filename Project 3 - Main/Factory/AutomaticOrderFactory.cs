using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    class AutomaticOrderFactory
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
    }
}
