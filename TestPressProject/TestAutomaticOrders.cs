using System;
using System.Collections.Generic;
using System.Text;
using Project_3___Press_Project;
using System.Linq;
using NUnit.Framework;


namespace TestPressProject
{
    public class TestAutomaticOrders
    {
        public Newspaper Newspaper { get; set; }
        public UserShop UserShop { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndDate { get; set; }
        public AutomaticOrder AutomaticOrder { get; set; }

        [SetUp]
        public void Setup()
        {
            Newspaper = new Newspaper();
            Newspaper.EAN13 = "1234";
            Newspaper.Name = "Journal";
            Newspaper.Periodicity = 5;
            Newspaper.Price = 5.00m;

            List<UserShop> userShops = new List<UserShop>() { UserShop };
            Shop shop = new Shop() { Adress = null, Name = "Shop", Orders = null, ShopCatalogs = null, UserShops = userShops };
            User user = new User() { Function = "Directeur", Login = "Pat", Name = "Patrick", UserShops = userShops, Password = "1234" };

            UserShop = new UserShop();
            UserShop.Shop = shop;
            UserShop.User = user;

            StartingDate = DateTime.Now;
            EndDate = StartingDate + TimeSpan.FromDays(7);

            AutomaticOrder = new AutomaticOrder();
            AutomaticOrder.EndDate = EndDate;
            AutomaticOrder.StartingDate = StartingDate;
            AutomaticOrder.Newspaper = Newspaper;
            AutomaticOrder.Shop = shop;
            AutomaticOrder.User = user;
        }

        [Test]
        public void TestAutomaticOrderAddition()
        {
            AutomaticOrder ao = new AutomaticOrder();
            ao = AutomaticOrder.CreateAutomaticOrder(UserShop, Newspaper, StartingDate, EndDate);
            Assert.AreEqual(AutomaticOrder, ao);
        }

    }
}
