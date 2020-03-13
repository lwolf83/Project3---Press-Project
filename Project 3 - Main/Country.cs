using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class Country
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
    }
}