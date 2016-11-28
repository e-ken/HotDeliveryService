using System;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;

namespace HotDeliveryService {
    public class AllExceptionFilter : ExceptionFilterAttribute {
        public override void OnException (HttpActionExecutedContext actionExecutedContext) {

            actionExecutedContext.Response = new HttpResponseMessage (HttpStatusCode.InternalServerError) {
                Content = new StringContent ("InternalServerError")
            };

        }
    }
}
