using System;
using System.Web.Configuration;

namespace HotDeliveryService {
    public static class Storage {

        private static IStorage storage;

        public static IStorage Get (string storageType) {
            if (storage == null) {
                if (storageType == "SQLite")
                    storage = new SQLiteStorage ();
                else
                    storage = new XmlStorage ();
            }
            return storage;
        }

    }
}
