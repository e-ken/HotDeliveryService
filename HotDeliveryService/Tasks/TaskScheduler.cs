using System;

using System.Threading;
using System.Web.Configuration;


namespace HotDeliveryService {
    public class TaskScheduler {

        private Random random;
        private Timer timer;
        private int N, M;

        public TaskScheduler () {
            N = Convert.ToInt32 (WebConfigurationManager.AppSettings ["IntervalStart"]);
            M = Convert.ToInt32 (WebConfigurationManager.AppSettings ["IntervalEnd"]);

            random = new Random (Environment.TickCount);
        }

        public void Start () {
            timer = new Timer (TimerCallback, null, random.Next (N * 1000, M * 1000), Timeout.Infinite);
        }

        private void TimerCallback (object state) {

            CreateDeliveriesJob.Execute ();
            ExpireDeliveriesJob.Execute ();

            timer.Change (random.Next (N * 1000, M * 1000), Timeout.Infinite);
        }

    }
}
