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

            CSVImporter importer = new CSVImporter();
            importer.BaseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            IEnumerable<Department> provinces = importer.ImportDepartments();

            foreach(var p in provinces)
            {
                Console.WriteLine(p);
            }
        }
    }
}
