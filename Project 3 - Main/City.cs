using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class City
    {
        public Guid CityId { get; set; }
        public string DepartmentCode { get; set; }
        public string ZipCode { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
    }
}