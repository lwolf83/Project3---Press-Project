using System;
using Project_3___Press_Project.Factory;
using Project_3___Press_Project.Entity;

namespace Project_3___Press_Project.Repository
{
    public class AutomaticOrderRepository : RepositoryBase<AutomaticOrder>
    {
        public AutomaticOrderRepository CreateAutomaticOrderInDB(Shop selectedShop, Newspaper newspaper, DateTime startDate, DateTime endDate, int quantity)
        {
            User user = UserSingleton.Instance.User;
            Shop shop = selectedShop;
            /*
            AutomaticOrderRepository automaticOrder = AutomaticOrderFactory.CreateAutomaticOrder(user, shop, newspaper, startDate, endDate, quantity);
            automaticOrder.SaveInDB();
            return automaticOrder;
            */
            throw new NotImplementedException("Reimplemente CreateAutomaticOrderInDB (move it)");
        }

        public override bool Exists(AutomaticOrder entity)
        {
            throw new NotImplementedException();
        }
    }
}
