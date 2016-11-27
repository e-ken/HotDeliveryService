using System;
//using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using System.Diagnostics.Contracts;

namespace HotDeliveryService {
    public class DeliveryController : ApiController {

        private readonly IStorage storage;

        public DeliveryController () {
            this.storage = Storage.Get (System.Web.Configuration.WebConfigurationManager.AppSettings ["StorageType"]);
            Contract.Assert (this.storage != null);
        }

        public IEnumerable<Delivery> GetAvailableDeliveries () {
            return storage.GetAvailableDeliveries ();
        }

        public void TakeDelivery (int userId, int deliveryId) {
            var delivery =  storage.GetDeliveryByDeliveryId (deliveryId); 

            if (delivery == null) return;
            if (delivery.Status != DeliveryStatus.Available) return;

            delivery.UserId = userId;
            delivery.Status = DeliveryStatus.Taken;

            storage?.Update (delivery);
        }
    }
}
