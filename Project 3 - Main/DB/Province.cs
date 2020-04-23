using System;
using System.Collections.Generic;

namespace Project_3___Press_Project
{
    public class Province
    {
        public Guid ProvinceId { get; set; }
        public string ProvinceCode { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public virtual ICollection<Department> Departments { get; set; }

        public override string ToString()
        {
            return $"{Name} - {ProvinceCode}";
        }
    }
}