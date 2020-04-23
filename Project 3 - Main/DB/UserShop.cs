using System;

namespace Project_3___Press_Project
{
    public class UserShop
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
