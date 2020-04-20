using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public class UserShopsLoader
    {
        public static UserShop GetUserShopFromUser(User user)
        {
            UserShop userShop = null;
            using (var context = new PressContext())
            {
                userShop = (from us in context.UserShops
                            join u in context.Users on us.User.UserId equals u.UserId
                            //join s in context.Shops on us.Shop.ShopId equals s.ShopId
                            select us).FirstOrDefault();
            }
            return userShop;
        }
    }
}
