using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class Order
    {
        public DateTime Date { get; set; }
        public Newspaper Newspaper {get; set;}
        public AbstractStructure Structure { get; set; }
        public User User { get; set; }
        public int Quantity { get; set; }
    }
}