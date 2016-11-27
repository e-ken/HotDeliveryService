using System;
using System.Collections.Generic;

namespace HotDeliveryService {
    public interface IStorage {
        IEnumerable<Delivery> GetAvailableDeliveries ();
        Delivery GetDeliveryByDeliveryId (int deliveryId);
        void Add (Delivery delivery);
        IEnumerable<Delivery> GetExpiredDeliveries ();
        void Update (Delivery delivery);
    }
}
