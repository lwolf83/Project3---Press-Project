using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public sealed class UserSingleton
    {
        /// <summary>
        /// Current user actions (connect / disconnect / etc)
        /// </summary>
        private static UserSingleton instance = null;

        public User User { get; private set; } = new User();
        public bool IsAuthenticated { get; private set; } = false;

        private UserSingleton()
        {
        }        

        public static UserSingleton GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserSingleton();
                }
                return instance;
            }
        }

        public void Init(string login, string password)
        {
            IAuthentification authentification = new Authentification();
            IsAuthenticated = authentification.LoginUsers(login, password);
            if(IsAuthenticated)
            {
                using (var context = new PressContext())
                {
                    User = context.Users.ToList().Where(u => u.Login == login).FirstOrDefault();
                }
            }
        }

        public void Disconnect()
        {
            instance = null;
            User = null;
            IsAuthenticated = false;
        }

        public IEnumerable<Shop> GetShops()
        {
            using (var context = new PressContext())
            {
                List<Shop> Shops = context.Shops.ToList();
                List<UserShop> UserShops = context.UserShops.ToList();

                var currentShop =  (from u in UserShops
                                    join s in Shops on u.ShopId equals s.ShopId
                                   where u.UserId == UserSingleton.GetInstance.User.UserId
                                   select s).ToList();

                return currentShop;
            }

        }

        public IEnumerable<Catalog> GetCatalog()
        {
            using (var context = new PressContext())
            {
                List<Newspaper> newspapers = context.Newspapers.ToList();
                List<Catalog> catalogs = context.Catalogs.ToList();
                return catalogs;
            }
           
        }

    }
}
