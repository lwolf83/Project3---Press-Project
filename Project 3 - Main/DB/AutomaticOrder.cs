using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public partial class AutomaticOrder
    {
        public Guid AutomaticOrderId { get; set; }
        public Newspaper Newspaper { get; set; }
        public Shop Shop { get; set; }
        public User User { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Quantity { get; set; }
    }
}
