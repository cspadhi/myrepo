using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using NorthwindServices.ServiceContracts;
using System.Xml.Linq;

namespace NorthwindServices.Services
{
    [ServiceBehavior(Namespace = "http://dotnetmentors.com/services/order")]  
    public class OrderService : IOrderService
    {
        public string GetOrderDate(int orderID)
        {
            XDocument doc = XDocument.Load("C:\\Orders.xml");

            string orderDate =
                (from result in doc.Descendants("DocumentElement")
                .Descendants("Orders")
                 where result.Element("OrderID").Value == orderID.ToString()
                 select result.Element("OrderDate").Value)
                .FirstOrDefault<string>();

            return orderDate;
        }

        public string GetOrderAmount(int orderID)
        {
            XDocument doc = XDocument.Load("C:\\Orders.xml");

            string orderTotal =
                (from result in doc.Descendants("DocumentElement")
                .Descendants("Orders")
                 where result.Element("OrderID").Value == orderID.ToString()
                 select result.Element("OrderTotal").Value)
                .FirstOrDefault<string>();

            return orderTotal;
        }

        public string GetShipCountry(int orderID)
        {
            XDocument doc = XDocument.Load("C:\\Orders.xml");

            string shipCountry =
                (from result in doc.Descendants("DocumentElement")
                .Descendants("Orders")
                 where result.Element("OrderID").Value == orderID.ToString()
                 select result.Element("ShipCountry").Value)
                .FirstOrDefault<string>();

            return shipCountry;
        }
    }
}
