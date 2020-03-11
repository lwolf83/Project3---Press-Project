using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class ShopCatalog
    {
        public Guid ShopID { get; set; }
        public Shop Shop { get; set; }
        public Guid CatalogId { get; set; }
        public ICollection<Catalog> Catalogs { get; set; }
        public int Quantity { get; set; }
    }
}