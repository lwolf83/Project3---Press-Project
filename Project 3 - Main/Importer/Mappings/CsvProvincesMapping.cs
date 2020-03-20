using System;
using System.Collections.Generic;
using System.Text;
using TinyCsvParser.Mapping;

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
