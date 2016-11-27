using System;
using System.Collections.Generic;
using SQLite;

namespace HotDeliveryService {
    public class SQLiteStorage : IStorage {

        public readonly string databasePath = "deliveries.db";

        private SQLiteConnection connection;

        public SQLiteStorage () {
            connection = new SQLiteConnection (databasePath);
            connection.CreateTable<Delivery> (CreateFlags.AutoIncPK);
        }

        public IEnumerable<Delivery> GetAvailableDeliveries () {
            return connection.Table<Delivery> ()
                             .Where (item => item.Status == DeliveryStatus.Available);
        }

        public Delivery GetDeliveryByDeliveryId (int deliveryId) {
            return connection.Find<Delivery> (deliveryId);
        }

        public void Add (Delivery delivery) {
            connection.Insert (delivery);
        }

        public IEnumerable<Delivery> GetExpiredDeliveries () {

            return from item in connection.Table<Delivery> ()
                   where DateTime.Now.Subtract (item.CreationTime).TotalSeconds > item.ExpirationTime
                   select item;     
        }

        public void Update (Delivery delivery) {
            delivery.ModificationTime = DateTime.Now;
            connection.Update (delivery);
        }
    }
}