using System.Web;
using System.Web.Http;


namespace HotDeliveryService {
    public class Global : HttpApplication {
        protected void Application_Start () {

            GlobalConfiguration.Configure (WebApiConfig.Register);

            TaskScheduler task = new TaskScheduler ();
            task.Start ();
        }
    }
}
