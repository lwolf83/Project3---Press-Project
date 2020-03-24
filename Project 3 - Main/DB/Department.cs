using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class Department
    {
        public Guid DepartmentId { get; set; }
        public string ProvinceCode { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public Province Province { get; set; }
        public virtual ICollection<City> Cities { get; set; }

        public override string ToString()
        {
            return $"{DepartmentName} - {ProvinceCode} - {DepartmentCode}";
        }
    }
}
