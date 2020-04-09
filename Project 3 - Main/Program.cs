using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_3___Press_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            ContextPopulator populator = new ContextPopulator();
            //populator.Populate();

            // Creates an Automatic order
            UserShop userShop = new UserShop();
            Newspaper newspaper = new Newspaper();

            using (var context = new PressContext())
            {
                var userS = (from us in context.UserShops
                            orderby us.ShopId
                            select us).ToList();

                var users = (from u in context.Users
                            select u).ToList();

                var shops = (from s in context.Shops
                             select s).ToList();

                userShop = (from us in userS
                            join u in users on us.User.UserId equals u.UserId
                            join s in shops on us.Shop.ShopId equals s.ShopId
                            select us).First();

                newspaper = (from n in context.Newspapers
                             orderby n.NewspaperId
                             select n).First();
            }

            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate + TimeSpan.FromDays(500);

            AutomaticOrder automaticOrder = new AutomaticOrder();
            automaticOrder.CreateAutomaticOrderInDB(userShop, newspaper, startDate, endDate, 123);

            // use the automaticOrders to create commands
            Catalog catalog = new Catalog();
            using(var context = new PressContext())
            {
                catalog = (from c in context.Catalogs
                            where c.Newspaper.NewspaperId == newspaper.NewspaperId
                            select c).FirstOrDefault();

            }
            catalog.Newspaper = newspaper;

            automaticOrder.FromACatalogCreateCommandsWithAutomaticOrders(catalog);

            Console.WriteLine("fini");
        }
    }
}
