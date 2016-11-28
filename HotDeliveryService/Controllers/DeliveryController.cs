using System;
using System.Net;
using System.Net.Http;
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

        public HttpResponseMessage TakeDelivery (int userId, int deliveryId) {
            var delivery =  storage.GetDeliveryByDeliveryId (deliveryId);

            if (delivery == null)
                return Request.CreateResponse (HttpStatusCode.NotFound, $"доставка id {deliveryId} не найдена");
            if (delivery.Status != DeliveryStatus.Available) 
                return Request.CreateResponse ((HttpStatusCode)422, $"доставка id {deliveryId} статус отличается от Available");


            delivery.UserId = userId;
            delivery.Status = DeliveryStatus.Taken;

            storage?.Update (delivery);

            return Request.CreateResponse (HttpStatusCode.OK);
        }
    }
}
