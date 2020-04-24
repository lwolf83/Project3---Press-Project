using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_3___Press_Project
{
    public class AutomaticOrderLoader
    {
        public static List<AutomaticOrder> GetFromShop(List<Shop> shops)
        {
            using(var context = new PressContext())
            {
                
                List<Shop> shops1 = context.Shops.Where(x => shops.Contains(x)).ToList();
                List<AutomaticOrder> automaticOrders = context.AutomaticOrders.Where(x => shops.Contains(x.Shop)).ToList();
                List<Newspaper> newspapers = context.Newspapers.ToList();
                return automaticOrders;
            }
        }

    }
}
