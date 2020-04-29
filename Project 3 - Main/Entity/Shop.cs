using System;
using System.Collections.Generic;

namespace Project_3___Press_Project.Entity
{
    public class Shop : EntityBase
    {
        public Guid ShopId { get; set; }
        public String Name { get; set; }
        public PostalAddress Address { get; set; }

        public ICollection<UserShop> UserShops { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ShopCatalog> ShopCatalogs { get; set; }
    }
}