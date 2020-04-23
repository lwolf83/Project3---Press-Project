using System;

namespace Project_3___Press_Project
{
    public class AutomaticOrderFactory
    {

        public static AutomaticOrder CreateAutomaticOrder(User user, Shop shop, Newspaper newspaper, DateTime startDate, DateTime endDate, int quantity)
        {
            AutomaticOrder ao = new AutomaticOrder();
            ao.Newspaper = newspaper;
            ao.StartingDate = startDate;
            ao.EndDate = endDate;
            ao.User = user;
            ao.Shop = shop;
            ao.Quantity = quantity;

            return ao;
        }

       
        

    }
}
