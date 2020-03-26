using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Shop Shop { get; set; }
        public Guid ShopId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }

        public ICollection<OrderCatalog> OrderCatalogs { get; set; }
    }
}