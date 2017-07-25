using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using NorthwindServices.ServiceContracts;
using System.Xml.Linq; 

namespace NorthwindServices.Services
{
    [ServiceBehavior(Namespace = "http://dotnetmentors.com/services/customer")]  
    public class CustomerService : ICustomerService 
    {
        public string GetCustomerName(int CustomerID)
        {
            XDocument doc = XDocument.Load("C:\\Customers.xml");

            string companyName =
                (from result in doc.Descendants("DocumentElement")
                .Descendants("Customer")
                 where result.Element("CustomerID").Value == CustomerID.ToString()
                 select result.Element("CompanyName").Value)
                .FirstOrDefault<string>();

            return companyName;
        }

        public string GetCustomerCity(int CustomerID)
        {
            XDocument doc = XDocument.Load("C:\\Customers.xml");

            string companyName =
                (from result in doc.Descendants("DocumentElement")
                .Descendants("Customer")
                 where result.Element("CustomerID").Value == CustomerID.ToString()
                 select result.Element("City").Value)
                .FirstOrDefault<string>();

            return companyName;
        }

        public int GetCustomerCount()
        {
            XDocument doc = XDocument.Load("C:\\Customers.xml");

            return doc.Descendants("Customer").Count();  
        }
    }
}
