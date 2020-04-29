using System;

namespace Project_3___Press_Project.Entity
{
    public class PostalAddress : EntityBase
    {
        public Guid PostalAddressId { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public City City { get; set; }
    }
}