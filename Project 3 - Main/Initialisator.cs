using System.Threading;
using System.Collections.Generic;
using System;
using System.Linq;
using Project_3___Press_Project.Repository;

namespace Project_3___Press_Project
{
    public static class Initialisator
    {
        public static void OnApplicationStart()
        {
            var loadAllShops = new ThreadStart(OnLoadAllShopStart);
            var loadLatestCatalogs = new ThreadStart(OnLoadLatestCatalogs);
            var loadLatestOrderCatalogs = new ThreadStart(OnLoadLatestOrderCatalogs);
            List<Thread> threads = new List<Thread>()
            {
                new Thread(loadAllShops),
                new Thread(loadLatestCatalogs),
                new Thread(loadLatestOrderCatalogs)
            };
            threads.ForEach(t => t.Start());
        }

        private static void OnLoadAllShopStart()
        {
            ShopRepository shopRepository = new ShopRepository();
            UserSingleton.Instance.AllShops = shopRepository.FindAll();
        }

        private static void OnLoadLatestCatalogs()
        {
            UserSingleton.Instance.LatestCatalogs = CatalogLoader.GetLatests(DateTime.Today - TimeSpan.FromDays(7));
        }

        private static void OnLoadLatestOrderCatalogs()
        {
            UserSingleton.Instance.LatestOrderCatalogs = UserSingleton.Instance.GetOrderCatalogs().Where(o => o.CreatedAt >= DateTime.Today).ToList();
        }
    }
}
