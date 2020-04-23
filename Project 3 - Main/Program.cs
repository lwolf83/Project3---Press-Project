using System;

namespace Project_3___Press_Project
{
    static class Program
    {
        static void Main(string[] args)
        {
            ContextPopulator populator = new ContextPopulator();
            populator.Populate();

            Console.WriteLine("fini");
        }
    }
}
