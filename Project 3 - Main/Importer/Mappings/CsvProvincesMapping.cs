using TinyCsvParser.Mapping;
using Project_3___Press_Project.Entity;

namespace Project_3___Press_Project.Importer.Mappings
{
    public class CsvProvincesMapping : CsvMapping<Province>
    {

        public CsvProvincesMapping() : base()
        {
            MapProperty(0, p => p.ProvinceCode);
            MapProperty(1, p => p.Name);
        }

    }
}
