﻿using System;
using System.Collections.Generic;

namespace Project_3___Press_Project
{
    public partial class Shop
    {
        public Guid ShopId { get; set; }
        public string Name { get; set; }
        public Address Adress { get; set; }

        public ICollection<UserShop> UserShops { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ShopCatalog> ShopCatalogs { get; set; }


    }
}