using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_3___Press_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            DataCreation.CreateData();

            //List<string[]> regions = CSVDataReader.GetDataEntries(@"..\..\..\..\regions.csv");
            //foreach (string[] entry in regions)
            //{
            //    foreach (string str in entry)
            //    {
            //        Console.Write(str + " ");
            //    }
            //    Console.WriteLine();
            //}
        }
    }
}
