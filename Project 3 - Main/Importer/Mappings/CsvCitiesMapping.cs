using TinyCsvParser.Mapping;
using Project_3___Press_Project.Entity;

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
