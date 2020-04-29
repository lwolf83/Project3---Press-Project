using System;

namespace Project_3___Press_Project.Entity
{
    public class AutomaticOrder : EntityBase
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
