using System;

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

        public void SaveInDB()
        {
            using (var context = new PressContext())
            {
                context.Update(this);
                context.SaveChanges();
            }
        }


        public void Delete()
        {
            using (var context = new PressContext())
            {
                context.AutomaticOrders.Remove(this);
                context.SaveChanges();
            }
        }
    }
}
