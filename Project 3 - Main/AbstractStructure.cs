using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public abstract class AbstractStructure
    {
        public int IdStructure { get; set; }
        public string Name { get; set; }
        public Adress Adress { get; set; }
        public List<AbstractStructure> Structures { get; set; }
        public List<Newspaper> Newspaper { get; set; }
        public List<User> Users { get; set; }
        public List<Order> Orders { get; set; }
        public decimal Incomes { get; set; }
    }
}