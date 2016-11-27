using System;

using System.Collections.Generic;
using System.Linq;

namespace HotDeliveryService {
    public static class ExpireDeliveriesJob {

        public static void Execute () {

            IStorage storage = Storage.Get (System.Web.Configuration.WebConfigurationManager.AppSettings ["StorageType"]);

            foreach (var item in storage.GetExpiredDeliveries ()) {
                item.Status = DeliveryStatus.Expired;
                storage.Update (item);
            }
        }
    }
}
