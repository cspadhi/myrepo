using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Security;
using WCFAuthenticationClient.ServiceReferenceClient;

namespace WCFAuthenticationClient.Models
{
    public class HelloClient
    {
        public string HelloWorld()
        {
            try
            {
                ServiceReferenceClient.HelloServiceClient client = new HelloServiceClient();
                client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

                client.ClientCredentials.UserName.UserName = "acc1";
                client.ClientCredentials.UserName.Password = "123";

                return client.HelloWorld();
            }
            catch
            {
                return null;
            }
        }
    }
}