using System;
using System.Collections.Generic;

namespace Project_3___Press_Project.Entity
{
    public class Department
    {
        public Guid DepartmentId { get; set; }
        public string ProvinceCode { get; set; }
        public string DepartmentCode { get; set; }
        public string Name { get; set; }
        public Province Province { get; set; }
        public virtual ICollection<City> Cities { get; set; }

        public override string ToString()
        {
            return $"{Name} - {ProvinceCode} - {DepartmentCode}";
        }
    }
}
