using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class Adress
    {
        public Guid AdressId { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public City City { get; set; }
    }
}