using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public partial class Catalog
    {
        public Guid CatalogId { get; set; }
        public Newspaper Newspaper { get; set; }
        public IEnumerable<OrderCatalog> OrderCatalogs { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Name { get; set; }
        public ICollection<ShopCatalog> ShopCatalogs { get; set; }
    }
}