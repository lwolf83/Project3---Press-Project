using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class Catalog
    {
        public Guid CatalogId { get; set; }
        public Newspaper Newspaper { get; set; }
        public OrderCatalog OrderCatalog { get; set; }
        public DateTime PublicationDate { get; set; }

        public ICollection<ShopCatalog> ShopCatalogs { get; set; }
    }
}