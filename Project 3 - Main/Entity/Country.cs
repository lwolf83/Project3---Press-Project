using System;
using System.Collections.Generic;

namespace Project_3___Press_Project.Entity
{
    public class Country
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Province> Province { get; set; }
    }
}