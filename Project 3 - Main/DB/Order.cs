using System;
using System.Collections.Generic;

namespace Project_3___Press_Project
{
    public partial class Order
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Shop Shop { get; set; }
        public Guid ShopId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string State { get; set; }

        public ICollection<OrderCatalog> OrderCatalogs { get; set; }
    }
}