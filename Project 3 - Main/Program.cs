using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_3___Press_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            /*CSVImporter importer = new CSVImporter();
            importer.BaseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            IEnumerable<City> cities = importer.ImportCities();

            foreach(var c in cities)
            {
                Console.WriteLine(c);
            }*/

            /*CSVImporter importer = new CSVImporter();
            importer.BaseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            IEnumerable<Province> provinces = importer.ImportProvinces();

            foreach (var c in provinces)
            {
                Console.WriteLine(c);
            }*/

            ContextPopulator populator = new ContextPopulator();
            populator.Populate();
            Console.WriteLine("fini");
        }
    }
}
