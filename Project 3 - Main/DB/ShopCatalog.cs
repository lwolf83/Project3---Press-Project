using System;

namespace Project_3___Press_Project
{
    public class ShopCatalog
    {
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
        public Guid CatalogId { get; set; }
        public Catalog Catalog { get; set; }
        public int Quantity { get; set; }
    }
}