using System.Threading;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Project_3___Press_Project
{
    public static class Initialisator
    {
        private static List<Thread> _threads;

        public static void OnApplicationStart()
        {
            var loadAllShops = new ThreadStart(OnLoadAllShopStart);
            var loadLatestCatalogs = new ThreadStart(OnLoadLatestCatalogs);
            var loadLatestOrderCatalogs = new ThreadStart(OnLoadLatestOrderCatalogs);
            _threads = new List<Thread>()
            {
                new Thread(loadAllShops),
                new Thread(loadLatestCatalogs),
                new Thread(loadLatestOrderCatalogs)
            };
            _threads.ForEach(t => t.Start());
        }

        public static void Finish()
        {
            _threads.ForEach(t => t.Join());
        }

        private static void OnLoadAllShopStart()
        {
            UserSingleton.GetInstance.AllShops = ShopsLoader.GetAll();
        }

        private static void OnLoadLatestCatalogs()
        {
            UserSingleton.GetInstance.LatestCatalogs = CatalogLoader.GetLatests(DateTime.Today - TimeSpan.FromDays(7));
        }

        private static void OnLoadLatestOrderCatalogs()
        {
            UserSingleton.GetInstance.LatestOrderCatalogs = UserSingleton.GetInstance.GetOrderCatalogs().Where(o => o.CreatedAt >= DateTime.Today).ToList();
        }
    }
}
