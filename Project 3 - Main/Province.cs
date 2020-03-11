using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class Province
    {
        public Guid ProvinceId { get; set; }
        public string Name { get; set; }
        public ICollection<City> Cities {get ; set; }
        public Country Country { get; set; }
    }
}