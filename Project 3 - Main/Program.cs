using System;

namespace Project_3___Press_Project
{
    static class Program
    {
        static void Main(string[] args)
        {
            var populator = new ContextPopulator();
            populator.Populate();
            var elapsedTime = populator.ElapsedTime;
            Console.WriteLine("Finished in {0:00} minute(s), {1:00} second(s) and {2:000} millisecond(s)", elapsedTime.Minutes,
                                                                     elapsedTime.Seconds,
                                                                     elapsedTime.Milliseconds);
            Console.ReadLine();
        }
    }
}
