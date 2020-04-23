using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_3___Press_Project
{
    public partial class AutomaticOrder
    {

        public AutomaticOrder CreateAutomaticOrderInDB(Shop selectedShop, Newspaper newspaper, DateTime startDate, DateTime endDate, int quantity)
        {
            User user = UserSingleton.GetInstance.User;
            Shop shop = selectedShop;
            AutomaticOrder automaticOrder = AutomaticOrderFactory.CreateAutomaticOrder(user, shop, newspaper, startDate, endDate, quantity);
            automaticOrder.SaveInDB();
            return automaticOrder;
        }

        private void SaveInDB()
        {
            using (var context = new PressContext())
            {
                context.Update(this);
                context.SaveChanges();
            }
        }

    }
}
