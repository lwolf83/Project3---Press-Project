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
            populator.Populate();
            ShopFilter filter = new ShopFilter();
            Console.WriteLine("fini");
        }
    }
}
