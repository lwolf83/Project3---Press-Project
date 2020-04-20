using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public class ShopsLoader
    {

        public static Shop FromUserShop(UserShop us)
        {
            Shop shop = null;
            using (var context = new PressContext())
            {
                shop = (from s in context.Shops
                        where s.ShopId == us.ShopId
                        select s).First();
            }
            return shop;
        }
    }
}
