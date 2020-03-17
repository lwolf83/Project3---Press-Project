﻿using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Project_3___Press_Project
{
    public class CSVDataReader
    {

        public static void PopulateDB_FromCSV()
        {
            using (var context = new PressContext())
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
                                          Province = context.Provinces.Where(p => p.ProvinceCode.Equals(departmentsData[i][0])).Single()
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
                                     Department = context.Departments.Where(d => d.DepartmentCode.Equals(citiesData[i][0])).Single()
                                 };
                context.AddRange(manyCities);
                context.SaveChanges();
            }
        }
        public static List<string[]> GetDataEntries(String filepath)
        {
            List<string[]> dataEntries = new List<string[]>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');
                    dataEntries.Add(columns);
                }
                dataEntries.RemoveAt(0);
                return dataEntries;
            }
        }
    }
}