using System;
using System.Collections.Generic;
using System.Text;
using TinyCsvParser.Mapping;

namespace Project_3___Press_Project.Importer.Mappings
{
    public class CsvCitiesMapping : CsvMapping<City>
    {
        public CsvCitiesMapping()
            : base()
        {
            MapProperty(0, city => city.DepartmentCode);
            MapProperty(1, city => city.ZipCode);
            MapProperty(2, city => city.Name);
        }
    }
}
