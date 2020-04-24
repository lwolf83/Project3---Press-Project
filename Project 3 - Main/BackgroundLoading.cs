using System.Threading;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Project_3___Press_Project
{
    public static class BackgroundLoading
    {
        private static List<Thread> _threads;

        public static void Load()
        {
            var loadThread = new ThreadStart(OnLoadThread);
            Thread thread = new Thread(loadThread);
            thread.Start();
        }

        private static void OnLoadThread()
        {
            LoadUserSingletonData();
            Thread.Sleep(5000);
            Load();
        }

        private static void LoadUserSingletonData()
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

        public static void Finish()
        {
            _threads.ForEach(t => t.Join());
        }
    }
}
