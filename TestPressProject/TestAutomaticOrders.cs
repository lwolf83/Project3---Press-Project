﻿using System;
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
        public User User { get; set; }
        public Shop Shop { get; set; }
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
            Shop = new Shop() { Adress = null, Name = "Shop", Orders = null, ShopCatalogs = null, UserShops = userShops };
            User = new User() { Function = "Directeur", Login = "Pat", Name = "Patrick", UserShops = userShops, Password = "1234" };

            UserShop = new UserShop();
            UserShop.Shop = Shop;
            UserShop.User = User;

            StartingDate = DateTime.Now;
            EndDate = StartingDate + TimeSpan.FromDays(7);

            AutomaticOrder = new AutomaticOrder();
            AutomaticOrder.EndDate = EndDate;
            AutomaticOrder.StartingDate = StartingDate;
            AutomaticOrder.Newspaper = Newspaper;
            AutomaticOrder.Shop = Shop;
            AutomaticOrder.User = User;
            AutomaticOrder.Quantity = 150;
        }

        [Test]
        public void TestAutomaticOrderAddition()
        {
            AutomaticOrder ao = new AutomaticOrder();
            ao = AutomaticOrderFactory.CreateAutomaticOrder(User, Shop, Newspaper, StartingDate, EndDate, 150);
            AutomaticOrderAllFieldsComparer.AssertAreEqual(AutomaticOrder, ao);
            // Cannot use the classical Assert.AreEqual or any other.
            // Need to compare directly the properties. See : https://stackoverflow.com/questions/6328218/unit-test-assert-areequal-failed
        }

        static class AutomaticOrderAllFieldsComparer
        {
            public static void AssertAreEqual(AutomaticOrder expected, AutomaticOrder actual)
            {
                Assert.AreEqual(expected.EndDate, actual.EndDate);
                Assert.AreEqual(expected.AutomaticOrderId, actual.AutomaticOrderId);
                Assert.AreEqual(expected.Newspaper, actual.Newspaper);
                Assert.AreEqual(expected.Shop, actual.Shop);
                Assert.AreEqual(expected.StartingDate, actual.StartingDate);
                Assert.AreEqual(expected.User, actual.User);
            }
        }

    }
}
