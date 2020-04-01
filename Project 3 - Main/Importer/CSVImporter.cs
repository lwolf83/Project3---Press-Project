using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using TinyCsvParser; // Library available here: https://bytefish.github.io/TinyCsvParser/sections/userguide/parser.html#contructing-a-parser 

namespace Project_3___Press_Project
{
    public class CSVImporter
    {
        public String BaseDirectoryPath { get; set; }
        public CsvParserOptions ParserOptions { get; private set; }

        public CSVImporter()
        {
            ParserOptions = new CsvParserOptions(false, ',');
        }

        public String CitiesCsvFilePath
        {
            get => Path.Combine(BaseDirectoryPath, "cities.csv");
        }

        public String DepartmentsCsvFilePath
        {
            get => Path.Combine(BaseDirectoryPath, "departments.csv");
        }

        public String ProvincesCsvFilePath
        {
            get => Path.Combine(BaseDirectoryPath, "regions.csv");
        }

        public IEnumerable<City> ImportCities()
        {
            var mapping = new Importer.Mappings.CsvCitiesMapping();
            var parser = new CsvParser<City>(ParserOptions, mapping);
            var results = parser.ReadFromFile(CitiesCsvFilePath, Encoding.UTF8);
            return ImportResultSet(results);
        }

        public IEnumerable<Department> ImportDepartments()
        {
            var mapping = new Importer.Mappings.CsvDepartmentMapping();
            var parser = new CsvParser<Department>(ParserOptions, mapping);
            var results = parser.ReadFromFile(DepartmentsCsvFilePath, Encoding.UTF8);
            return ImportResultSet(results);
        }

        public IEnumerable<Province> ImportProvinces()
        {
            var mapping = new Importer.Mappings.CsvProvincesMapping();
            var parser = new CsvParser<Province>(ParserOptions, mapping);
            var results = parser.ReadFromFile(ProvincesCsvFilePath, Encoding.UTF8);
            return ImportResultSet(results);
        }

        private IEnumerable<T> ImportResultSet<T>(IEnumerable<TinyCsvParser.Mapping.CsvMappingResult<T>> results)
        {
            foreach (var r in results)
            {
                if (r.IsValid)
                {
                    yield return r.Result;
                }
                else
                {
                    throw new FieldAccessException(r.Error.Value);
                }
            }
        }
    }
}
