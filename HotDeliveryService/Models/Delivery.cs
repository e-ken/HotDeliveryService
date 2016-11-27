using System;
using SQLite;

namespace HotDeliveryService {
    public class Delivery {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DeliveryStatus Status { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationTime { get; set; }
        public int ExpirationTime  { get; set; }
    }
}
