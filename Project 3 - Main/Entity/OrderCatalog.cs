using System;

namespace Project_3___Press_Project.Entity
{
    public class OrderCatalog : EntityBase
    {
        public override Guid Id { get => OrderCatalogId; }
        public Guid OrderCatalogId { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid CatalogId { get; set; }
        public Catalog Catalog { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
