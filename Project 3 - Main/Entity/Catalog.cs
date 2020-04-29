using System;
using System.Collections.Generic;

namespace Project_3___Press_Project.Entity
{
    public class Catalog : EntityBase
    {
        public Guid CatalogId { get; set; }
        public Newspaper Newspaper { get; set; }
        public IEnumerable<OrderCatalog> OrderCatalogs { get; set; }
        public DateTime PublicationDate { get; set; }
        public String Name { get; set; }
        public ICollection<ShopCatalog> ShopCatalogs { get; set; }
    }
}