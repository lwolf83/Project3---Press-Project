using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class OrderCatalog
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid CatalogId { get; set; }
        public Catalog Catalog { get; set; }
        public int Quantity { get; set; }
    }

}
