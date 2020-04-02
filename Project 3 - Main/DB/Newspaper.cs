using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class Newspaper 
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