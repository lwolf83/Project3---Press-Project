using System.Collections.Generic;
using System.Linq;
using Project_3___Press_Project.Entity;

namespace Project_3___Press_Project
{
    public static class ShopsLoader
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

        public static IEnumerable<Shop> FromUser(User user)
        {
            using (var context = new PressContext())
            {
                var currentShop = (from u in context.UserShops
                                   join s in context.Shops on u.ShopId equals s.ShopId
                                   where u.UserId == user.UserId
                                   select s).ToList();

                return currentShop;
            }
        }

    }
}
