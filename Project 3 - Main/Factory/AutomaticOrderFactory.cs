using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public class AutomaticOrderFactory
    {

        public static AutomaticOrder CreateAutomaticOrder(UserShop userShop, Newspaper newspaper, DateTime startDate, DateTime endDate, int quantity)
        {
            AutomaticOrder ao = new AutomaticOrder();
            ao.Newspaper = newspaper;
            ao.StartingDate = startDate;
            ao.EndDate = endDate;
            ao.User = UsersLoader.FromUserShop(userShop);
            ao.Shop = ShopsLoader.FromUserShop(userShop);
            ao.Quantity = quantity;

            return ao;
        }

       
        

    }
}
