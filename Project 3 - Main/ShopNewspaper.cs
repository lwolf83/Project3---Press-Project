using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class ShopNewspaper
    {
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
        public Guid NewspaperId { get; set; }
        public Newspaper Newspaper { get; set; }

        public int Quantity { get; set; }
    }
}