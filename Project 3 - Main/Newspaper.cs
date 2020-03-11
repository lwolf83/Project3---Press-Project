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
        
        public ICollection<ShopNewspaper> ShopNewspaper { get; set; }
        public ICollection<OrderNewspaper> OrderNewspaper { get; set; }

    }
}