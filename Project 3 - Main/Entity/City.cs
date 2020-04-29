using System;

namespace Project_3___Press_Project.Entity
{
    public class City : EntityBase
    {
        public Guid CityId { get; set; }
        public string DepartmentCode { get; set; }
        public string ZipCode { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
    
        public override String ToString()
        {
            return $"{Name} - {DepartmentCode} - {ZipCode}";
        }
    }
}