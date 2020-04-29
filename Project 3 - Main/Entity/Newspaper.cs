using System;
using System.Collections.Generic;

namespace Project_3___Press_Project.Entity
{
    public class Newspaper : EntityBase
    {
        public Guid NewspaperId { get; set; }
        public string Name { get; set; }
        public string EAN13 { get; set; }
        public decimal Price { get; set; }
        public Editor Editor { get; set; }
        public int Periodicity { get; set; }
        public ICollection<Catalog> Catalogs { get; set; }
    }
}