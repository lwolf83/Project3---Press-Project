using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Project_3___Press_Project
{
    public class DataCreation
    {
        public static void CreateData()
        {
            using (var context = new PressContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                List<City> cities = new List<City>();
                cities = DataCreation.PopulateDBLocations_FromCSV(context);

                Random randomGenerator = new Random();
                List<City> randomCities = new List<City>();
                for (int counter = 0; counter < 3; counter++)
                {
                    randomCities.Add(cities[randomGenerator.Next(0, cities.Count)]);
                }

                List<Adress> adresses = new List<Adress>();
                adresses = DataCreation.CreateAdresses(randomCities, 3);

                List<Shop> shops = new List<Shop>();
                shops = DataCreation.CreateShops(adresses);

                List<Editor> editors = new List<Editor>();
                editors = DataCreation.CreateEditors(3);

                List<Newspaper> newspapers = new List<Newspaper>();
                newspapers = DataCreation.CreateNewspapers(editors, 25);

                context.AddRange(adresses);
                context.AddRange(shops);
                context.AddRange(editors);
                context.AddRange(newspapers);

                context.SaveChanges();
            }
        }

        public static List<City> PopulateDBLocations_FromCSV(PressContext context)
        {
            var country = new Country { Name = "France" };
            context.Add(country);

            var provincesData = CSVDataReader.GetDataEntries(@"..\..\..\..\regions.csv");
            var manyProvinces = from i in Enumerable.Range(0, provincesData.Count)
                                select new Province
                                { ProvinceCode = provincesData[i][0], Name = provincesData[i][1], Country = country };
            context.AddRange(manyProvinces);
            context.SaveChanges();

            var departmentsData = CSVDataReader.GetDataEntries(@"..\..\..\..\departments.csv");
            var manyDepartments = from i in Enumerable.Range(0, departmentsData.Count)
                                    select new Department
                                    {
                                        ProvinceCode = departmentsData[i][0],
                                        DepartmentCode = departmentsData[i][1],
                                        DepartmentName = departmentsData[i][2],
                                        Province = context.Provinces.Where(p => p.ProvinceCode.Equals(departmentsData[i][0])).First()
                                    };
            context.AddRange(manyDepartments);
            context.SaveChanges();

            var citiesData = CSVDataReader.GetDataEntries(@"..\..\..\..\cities.csv");
            var manyCities = from i in Enumerable.Range(0, citiesData.Count)
                                select new City
                                {
                                    DepartmentCode = citiesData[i][0],
                                    ZipCode = citiesData[i][1],
                                    Name = citiesData[i][2],
                                    Department = context.Departments.Where(d => d.DepartmentCode.Equals(citiesData[i][0])).First()
                                };
            context.AddRange(manyCities);
            context.SaveChanges();

            return manyCities.ToList();
        }

        public static List<Adress> CreateAdresses(List<City> cities, int numberOfAdressesPerCity)
        {
            List<Adress> adresses = new List<Adress>();
            int adressesCounter = 0;
            foreach(City city in cities)
            {
                for (int i = 0; i<numberOfAdressesPerCity; i++)
                {
                    Adress adress = new Adress();
                    adress.StreetNumber = $"{numberOfAdressesPerCity}";
                    adress.StreetName = $"street n° {adressesCounter};{numberOfAdressesPerCity}";
                    adress.City = city;
                    adresses.Add(adress);
                    adressesCounter++;
                }
            }
            return adresses;
        }

        public static List<Shop> CreateShops (List<Adress> adresses)
        {
            List<Shop> shops = new List<Shop>();
            int shopCounter = 0;
            foreach (Adress adress in adresses)
            {
                Shop shop = new Shop();
                shop.Name = $"Shop n° {shopCounter}";
                shop.Adress = adress;
                shopCounter++;
                shops.Add(shop);
            }
            return shops;
        }

        public static List<Editor> CreateEditors(int numberOfDesiredEditors)
        {
            List<Editor> editors = new List<Editor>();
            for (int i = 0; i < numberOfDesiredEditors; i++)
            {
                Editor editor = new Editor();
                editor.Name = $"Editor n° {i}";
                editors.Add(editor);
            }
            return editors;
        }

        public static List<Newspaper> CreateNewspapers(List<Editor> editors, int numberOfNewspaperPerEditor)
        {
            Random randomGenerator = new Random();
            List<Newspaper> newspapers = new List<Newspaper>();
            int editorCounter = 0;
            foreach(Editor editor in editors)
            {
                for (int i =0; i<numberOfNewspaperPerEditor; i++)
                {
                    Newspaper newspaper = new Newspaper();
                    newspaper.Name = $"Newspaper n° {editorCounter}; {numberOfNewspaperPerEditor}";
                    newspaper.EAN13 = $"{editorCounter}{numberOfNewspaperPerEditor}{randomGenerator.Next(100, 10000)}";
                    newspaper.Price = randomGenerator.Next(1, 10);
                    newspaper.Editor = editor;
                    newspaper.Periodicity = randomGenerator.Next(1, 28);

                    newspapers.Add(newspaper);
                }
                editor.Newspapers = newspapers;
                editorCounter++;
            }
            return newspapers;
        }
    }
}
