using System;
using System.Collections.Generic;
using Project_3___Press_Project;
using NUnit.Framework;
using Project_3___Press_Project.Entity;
using Project_3___Press_Project.Factory;

namespace TestPressProject
{
    public class TestAutomaticOrders
    {
        public Newspaper Newspaper { get; set; }
        public UserShop UserShop { get; set; }
        public User User { get; set; }
        public Shop Shop { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndDate { get; set; }
        public AutomaticOrder AutomaticOrder { get; set; }

        [SetUp]
        public void Setup()
        {
            Newspaper = new Newspaper
            {
                EAN13 = "1234",
                Name = "Journal",
                Periodicity = 5,
                Price = 5.00m
            };

            List<UserShop> userShops = new List<UserShop>() { UserShop };
            Shop = new Shop() { Address = null, Name = "Shop", Orders = null, ShopCatalogs = null, UserShops = userShops };
            User = new User() { Function = "Directeur", Login = "Pat", Name = "Patrick", UserShops = userShops, Password = "1234" };

            UserShop = new UserShop
            {
                Shop = Shop,
                User = User
            };

            StartingDate = DateTime.Now;
            EndDate = StartingDate + TimeSpan.FromDays(7);

            AutomaticOrder = new AutomaticOrder
            {
                EndDate = EndDate,
                StartingDate = StartingDate,
                Newspaper = Newspaper,
                Shop = Shop,
                User = User,
                Quantity = 150
            };
        }

        [Test]
        public void TestAutomaticOrderAddition()
        {
            AutomaticOrder automaticOrder = AutomaticOrderFactory.CreateAutomaticOrder(User, Shop, Newspaper, StartingDate, EndDate, 150);

            MultiplePropertiesAssertion.AssertAreEqual(AutomaticOrder, automaticOrder);
        }
    }
}
