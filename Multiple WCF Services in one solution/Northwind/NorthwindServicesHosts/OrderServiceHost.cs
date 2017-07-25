using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using NorthwindServices.Services;

namespace NorthwindServicesHosts
{
    internal class OrderServiceHost
    {
        static ServiceHost serviceHost = null;
        public OrderServiceHost()
        {  }

        internal static void Start()
        {
            if (serviceHost != null)
                serviceHost.Close();
            
            serviceHost = new ServiceHost(typeof(OrderService));
            serviceHost.Open();
        }

        internal static void Stop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
