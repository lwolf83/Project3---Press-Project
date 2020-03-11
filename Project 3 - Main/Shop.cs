using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class Shop
    {
        public Guid ShopId { get; set; }
        public string Name { get; set; }
        public Adress Adress { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ShopNewspaper> Stocks { get; set; }

    }
}