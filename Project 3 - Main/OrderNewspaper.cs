using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class OrderNewspaper
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid NewspaperId { get; set; }
        public Newspaper Newspaper { get; set; }
        public int Quantity { get; set; }
    }

}
