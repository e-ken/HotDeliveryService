using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

namespace HotDeliveryService {
    public class XmlStorage : IStorage {

        public readonly string databasePath = "deliveries.xml";

        private List<Delivery> deliveries;

        public XmlStorage () {
            if (File.Exists (databasePath)) {
                XmlSerializer serializer = new XmlSerializer (typeof (List<Delivery>));
                using (System.IO.StreamReader stream = new StreamReader (databasePath))
                    deliveries = (List<Delivery>)serializer.Deserialize (stream);
            }
            if (deliveries == null) deliveries = new List<Delivery> ();

        }

        public IEnumerable<Delivery> GetAvailableDeliveries () {
            return from item in deliveries
                   where item.Status == DeliveryStatus.Available
                   select item;
        }

        public Delivery GetDeliveryByDeliveryId (int deliveryId) {
            return (from item in deliveries
                    where item.Id == deliveryId
                    select item).First();
        }

        public void Add (Delivery delivery) {
            delivery.Id = deliveries.Count + 1;
            deliveries.Add (delivery);
            lock(deliveries) Save();
        }

        public IEnumerable<Delivery> GetExpiredDeliveries () {
            return from item in deliveries
                   where item.Status == DeliveryStatus.Available &&
                             DateTime.Now.Subtract (item.CreationTime).TotalSeconds > item.ExpirationTime
                   select item;
        }

        public void Update (Delivery delivery) {
            delivery.ModificationTime = DateTime.Now;
            lock (deliveries) Save ();
        }

        public void Save () {
            XmlSerializer serializer = new XmlSerializer (typeof(List<Delivery>));
            using (StreamWriter stream = new StreamWriter (databasePath, false))
                serializer.Serialize (stream, deliveries);
        }
    }
}
