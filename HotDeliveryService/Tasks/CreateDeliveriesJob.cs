using System;

using System.Linq;
using System.Web.Configuration;

namespace HotDeliveryService {
    public static class CreateDeliveriesJob {

        public static void Execute () {

            IStorage storage = Storage.Get (System.Web.Configuration.WebConfigurationManager.AppSettings ["StorageType"]);

            int N = Convert.ToInt32 (WebConfigurationManager.AppSettings ["CountN"]);
            int M = Convert.ToInt32 (WebConfigurationManager.AppSettings ["CountM"]);

            for (int i = N; i <= M; i++) {
                Delivery delivery = new Delivery () {
                    Status = DeliveryStatus.Available,
                    CreationTime = DateTime.Now,
                    ExpirationTime = Convert.ToInt32 (WebConfigurationManager.AppSettings ["ExpirationTime"])
                };
                delivery.Title = $"Delivery created at {delivery.CreationTime}";

                storage.Add (delivery);
            }
        }
    }
}
