using TinyCsvParser.Mapping;

namespace Project_3___Press_Project.Importer.Mappings
{
    public class CsvDepartmentMapping : CsvMapping<Department>
    {
        public CsvDepartmentMapping() : base()
        {
            MapProperty(0, d => d.ProvinceCode);
            MapProperty(1, d => d.DepartmentCode);
            MapProperty(2, d => d.DepartmentName);

        }

    }
}
