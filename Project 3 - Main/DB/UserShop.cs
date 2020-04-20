using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public partial class UserShop
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
