using System;

namespace Project_3___Press_Project
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public City City { get; set; }
    }
}