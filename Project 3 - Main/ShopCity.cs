using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class ShopCity
    {
        public Shop Shop { get; set; }
        public string ShopName { get => Shop.Name; }
        public Guid ShopId { get => Shop.ShopId; }
        public City City { get; set; }
        public string CityName { get => City.Name; }
    }
}
